/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;

namespace graphicserver.Classes
{
     public static class CgXmlLayoutHelper
    {
        public static void UpdateOverflowedTextItem(
            XmlDocument doc,
            string itemId,
            string newText)
        {
            XmlNode itemNode = doc.SelectSingleNode($"//cg-item[@id='{itemId}']");
            if (itemNode == null)
                return;

            XmlNode textNode = itemNode.SelectSingleNode("text");
            XmlNode propsNode = itemNode.SelectSingleNode("cg-props");
            XmlNode originalRectNode = propsNode?.SelectSingleNode("Orignalrect");

            if (textNode == null || propsNode == null || originalRectNode == null)
                return;

            // Update text
            textNode.InnerText = newText;

            string fontName = textNode.Attributes["font"]?.Value ?? "Arial";
            float fontSize = float.Parse(textNode.Attributes["font-size"]?.Value ?? "20");

            int defaultX = GetInt(originalRectNode, "pos-x");
            int defaultY = GetInt(originalRectNode, "pos-y");
            int defaultWidth = GetInt(originalRectNode, "width");
            int defaultHeight = GetInt(originalRectNode, "height");

            int actualTextWidth = MeasureTextWidth(newText, fontName, fontSize);

            int overflow = Math.Max(0, actualTextWidth - defaultWidth);

            // Main title box
            int newX = defaultX - overflow;
            int newWidth = defaultWidth + overflow;

            SetCgProps(propsNode, newX, defaultY, newWidth, defaultHeight);

            // Move grouped items also
            XmlNode groupNode = propsNode.SelectSingleNode("Groupitem");

            if (groupNode != null && !string.IsNullOrWhiteSpace(groupNode.InnerText))
            {
                string[] groupItems = groupNode.InnerText
                    .Split(',')
                    .Select(x => x.Trim())
                    .Where(x => x.Length > 0)
                    .ToArray();

                foreach (string groupItemId in groupItems)
                {
                    MoveGroupedItemFromCurrentDefault(doc, groupItemId, overflow);
                }
            }
        }

        private static void MoveGroupedItemFromCurrentDefault(
            XmlDocument doc,
            string itemId,
            int overflow)
        {
            XmlNode itemNode = doc.SelectSingleNode($"//cg-item[@id='{itemId}']");
            XmlNode propsNode = itemNode?.SelectSingleNode("cg-props");

            if (propsNode == null)
                return;

            // If grouped item has Orignalrect, use that
            XmlNode originalRectNode = propsNode.SelectSingleNode("Orignalrect");

            int defaultX;
            int defaultY;
            int defaultWidth;
            int defaultHeight;

            if (originalRectNode != null)
            {
                defaultX = GetInt(originalRectNode, "pos-x");
                defaultY = GetInt(originalRectNode, "pos-y");
                defaultWidth = GetInt(originalRectNode, "width");
                defaultHeight = GetInt(originalRectNode, "height");
            }
            else
            {
                // If no Orignalrect exists, current XML values are treated as default
                defaultX = GetInt(propsNode, "pos-x");
                defaultY = GetInt(propsNode, "pos-y");
                defaultWidth = GetInt(propsNode, "width");
                defaultHeight = GetInt(propsNode, "height");
            }

            SetCgProps(
                propsNode,
                defaultX - overflow,
                defaultY,
                defaultWidth,
                defaultHeight);
        }

        private static int MeasureTextWidth(string text, string fontName, float fontSize)
        {
            bool isBold = fontName.ToLower().Contains("bold");
            string cleanFontName = fontName.Replace(" Bold", "").Trim();

            using (Bitmap bmp = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(bmp))
            using (Font font = new Font(
                cleanFontName,
                fontSize,
                isBold ? FontStyle.Bold : FontStyle.Regular))
            {
                return (int)Math.Ceiling(g.MeasureString(text, font).Width);
            }
        }

        private static int GetInt(XmlNode node, string attrName)
        {
            return Convert.ToInt32(node.Attributes[attrName].Value.Trim());
        }

        private static void SetCgProps(
            XmlNode propsNode,
            int x,
            int y,
            int width,
            int height)
        {
            propsNode.Attributes["pos-x"].Value = x.ToString();
            propsNode.Attributes["pos-y"].Value = y.ToString();
            propsNode.Attributes["width"].Value = width.ToString();
            propsNode.Attributes["height"].Value = height.ToString();
        }
    }
}
*/

using System;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace graphicserver.Classes
{
    public static class CgXmlLayoutHelper
    {
        public static void UpdateOverflowedTextItem(
            XmlDocument doc,
            string itemId,
            string newText)
        {
            XmlNode itemNode = doc.SelectSingleNode($"//cg-item[@id='{itemId}']");
            if (itemNode == null) return;

            XmlNode textNode = itemNode.SelectSingleNode("text");
            XmlNode propsNode = itemNode.SelectSingleNode("cg-props");
            XmlNode originalRectNode = propsNode?.SelectSingleNode("Orignalrect");

            if (textNode == null || propsNode == null || originalRectNode == null)
                return;

            textNode.InnerText = newText;

            string fontName = textNode.Attributes["font"]?.Value ?? "Arial";
            float fontSize = float.Parse(textNode.Attributes["font-size"]?.Value ?? "20");

            int defaultX = GetInt(originalRectNode, "pos-x");
            int defaultY = GetInt(originalRectNode, "pos-y");
            int defaultWidth = GetInt(originalRectNode, "width");
            int defaultHeight = GetInt(originalRectNode, "height");

            int endMark = GetIntOrDefault(originalRectNode, "endmark", defaultX + defaultWidth);

            int actualTextWidth = MeasureTextWidth(newText, fontName, fontSize);
            int newWidth = Math.Max(defaultWidth, actualTextWidth);

            // Keep original right edge behavior first
            int overflow = Math.Max(0, newWidth - defaultWidth);
            int newX = defaultX - overflow;

            // EndMark protection
            int rightEdge = newX + newWidth;
            int endMarkShift = Math.Max(0, rightEdge - endMark);

            newX -= endMarkShift;

            int totalShift = overflow + endMarkShift;

            SetCgProps(propsNode, newX, defaultY, newWidth, defaultHeight);

            XmlNode groupNode = propsNode.SelectSingleNode("Groupitem");
            if (groupNode == null || string.IsNullOrWhiteSpace(groupNode.InnerText))
                return;

            string[] groupItems = groupNode.InnerText
                .Split(',')
                .Select(x => x.Trim())
                .Where(x => x.Length > 0)
                .ToArray();

            foreach (string groupItemId in groupItems)
            {
                MoveGroupedItem(doc, groupItemId, totalShift);
            }
        }

        private static void MoveGroupedItem(
            XmlDocument doc,
            string itemId,
            int totalShift)
        {
            XmlNode itemNode = doc.SelectSingleNode($"//cg-item[@id='{itemId}']");
            XmlNode propsNode = itemNode?.SelectSingleNode("cg-props");

            if (propsNode == null) return;

            XmlNode originalRectNode = propsNode.SelectSingleNode("Orignalrect");

            int defaultX;
            int defaultY;
            int defaultWidth;
            int defaultHeight;

            if (originalRectNode != null)
            {
                defaultX = GetInt(originalRectNode, "pos-x");
                defaultY = GetInt(originalRectNode, "pos-y");
                defaultWidth = GetInt(originalRectNode, "width");
                defaultHeight = GetInt(originalRectNode, "height");
            }
            else
            {
                defaultX = GetInt(propsNode, "pos-x");
                defaultY = GetInt(propsNode, "pos-y");
                defaultWidth = GetInt(propsNode, "width");
                defaultHeight = GetInt(propsNode, "height");
            }

            SetCgProps(
                propsNode,
                defaultX - totalShift,
                defaultY,
                defaultWidth,
                defaultHeight);
        }

        private static int MeasureTextWidth(string text, string fontName, float fontSize)
        {
            bool isBold = fontName.ToLower().Contains("bold");
            string cleanFontName = fontName
                .Replace(" Bold", "")
                .Replace("Bold", "")
                .Trim();

            using (Bitmap bmp = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(bmp))
            using (Font font = new Font(
                cleanFontName,
                fontSize,
                isBold ? FontStyle.Bold : FontStyle.Regular))
            {
                return (int)Math.Ceiling(g.MeasureString(text, font).Width);
            }
        }

        private static int GetInt(XmlNode node, string attrName)
        {
            return Convert.ToInt32(node.Attributes[attrName].Value.Trim());
        }

        private static int GetIntOrDefault(XmlNode node, string attrName, int defaultValue)
        {
            XmlAttribute attr = node.Attributes[attrName];
            if (attr == null || string.IsNullOrWhiteSpace(attr.Value))
                return defaultValue;

            return Convert.ToInt32(attr.Value.Trim());
        }

        private static void SetCgProps(
            XmlNode propsNode,
            int x,
            int y,
            int width,
            int height)
        {
            propsNode.Attributes["pos-x"].Value = x.ToString();
            propsNode.Attributes["pos-y"].Value = y.ToString();
            propsNode.Attributes["width"].Value = width.ToString();
            propsNode.Attributes["height"].Value = height.ToString();
        }
    }
}
