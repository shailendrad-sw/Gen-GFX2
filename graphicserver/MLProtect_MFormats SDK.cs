//---------------------------------------------------------------------------
// MLProtect_MFormatsSDK.cs : Personal protection code for the Medialooks License system
//---------------------------------------------------------------------------
// Copyright (c) 2018, Medialooks Soft
// www.medialooks.com (dev@medialooks.com)
//
// Authors: Medialooks Team
// Version: 3.0.0.0
//
//---------------------------------------------------------------------------
// CONFIDENTIAL INFORMATION
//
// This file is Intellectual Property (IP) of Medialooks Soft and is
// strictly confidential. You can gain access to this file only if you
// sign a License Agreement and a Non-Disclosure Agreement (NDA) with
// Medialooks Soft. If you had not signed any of these documents, please
// contact <dev@medialooks.com> immediately.
//
//---------------------------------------------------------------------------
// Usage:
//
// 1. Add the reference to MLProxy.dll (located in the SDK /bin folder) in your C# project
//
// 2. Add this file to the project
//
// 3. Call MFormatsSDKLic.IntializeProtection() method before creating any Medialooks 
//    objects (for e.g. in Form_Load event handler)
//
// 4. Compile the project
//
// IMPORTANT: If you have several Medialooks products, don't forget to initialize
//            protection for all of them. For e.g.
//
//            MPlatformSDKLic.IntializeProtection();
//            DecoderlibLic.IntializeProtection();
//            etc.

using System;

    public class MFormatsSDKLic
    {        
        private static MLPROXYLib.CoMLProxyClass m_objMLProxy;
        private static string strLicInfo = @"[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={3051B1C8-1700-4B02-8015-9A94D8696798}
License.Key={3EEC7691-56DF-D666-93AD-DCE41853C089}
License.Name=MFormats Module
License.UpdateExpirationDate=January 18, 2022
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=5669AC2A0C0EF3BED0702E98A3C3DB36DEDC86FC7335EF4C42BF7F1E85074B717551E7A59F25A83E0105D61B3714B109556515E5E6EC5D273AD5F56170040064BD4DB56F033597DAC9C8D58A2A45BC3B581105B2B3CFA99E32EA87E708818D095782E6E04AD61D6A269D9C80988D65E07FC1F64BC39BFAD9965A03A061B6C432

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={2B7F9BEC-9C3A-479C-9640-15705CF76938}
License.Key={9CB231FF-495A-052C-6791-274F637FB1B7}
License.Name=MFRenderer Module
License.UpdateExpirationDate=January 18, 2022
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=601A47BBE9BB145DCEEB7B2599A2D499DA9A49A1863D29CD57FD0CC6C9CD888DB06B63DDADEA748DB027B32AF75328AF0E122860CF99172FD5770A738415C6B662AB73809BB8A19B6FA631EE6B18801C23CCF05D1540221E65D795E6203EDEF844590F143CABD7AA2BE737C9E63A2BDC943765132F7FFE30C2AAD67A19ADCF44

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={D9BED21B-1799-413E-BAD3-B6F74DB887B4}
License.Key={34F2017E-037D-8FE3-57E3-9D8325B0C770}
License.Name=MFReader Module
License.UpdateExpirationDate=January 18, 2022
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=317910F40B78BC307D82E1628DA876C2C7185D7A5DEAA092BF9124CEC5A35EA281A0D70F114FBF0F831F42667E43FAE19AD9B3B9004D8B3684AF3E0D31B33A2725BE7E7B9555F550715507E2D30BBB7B4DBBD1B8D6039399DAE56F33DDB5CA4E62CDC92D517E4DB24559EBBE9A4C462B4836D86E7015A89D13BB997BA557894A

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={6D9538D0-4B0B-44FC-A823-318066626BB0}
License.Key={AE6A024D-3335-8633-D3F0-21DEABF96764}
License.Name=MFWriter Module
License.UpdateExpirationDate=January 18, 2022
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=1314F880733D133F2637A40D9500E09B95F0F8D2396C2CCD05664C25DC051A3DA00608918785A11FBB337D132B28A6633509A07DECB1525B5CA6C0489D36940EBA40B7F0D3F080F01E30C1BC40859B39C6EF93D60B47BD413021548E666447CF3C20F2649459CDF918D7207CDB95D2C410AB708034947B5DBD8BCDA1FB445160

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={F6BB589A-A8D7-4462-9264-34BA2C1FC43D}
License.Key={EC0460F0-2E5C-4C9B-A8C7-2A537F0EA20B}
License.Name=Delay internal module
License.UpdateExpirationDate=January 18, 2022
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=1767EA3AA637854F36D304A6AFEC18D578EDC4D0BB35052D26F543858599E1573822BE073A83CCCBAB44183A9A081489034BC9CB7B01F79C696DF2F01A0AE5A16AA8EBAE43C81F17F2958A9E5872CEA56FF0B1EB1368D0AB080B5DED2CD76FD72E07039D83017B3BCF7D5B0E567BFB114BC4125EC727CDF18BE8C16B1C82AF39

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={5646E419-68EB-4F23-AA8D-45C06B815F2A}
License.Key={9D8B3946-24B4-C9DD-5E6A-20561F813085}
License.Name=MWebRTC module
License.UpdateExpirationDate=January 18, 2022
License.Edition=Standard gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=57ACC6B51CF603B295A481A56C58FF5C57885FAA8ECB2CB3C4C9CB6C5E5849FCEDF29F5C0C7C4143CC0AB81D6FDD93CA22575D823957ABFBDE24DA59EFEC54DAAA82C2F8653164F2E942360AFE1A01E274CA74A037ACC3306571EFF3BA0E87E97BF1119935F51944410A64BE238EF2732F2D4F646FD7A69223A3F84D65D1E0BD

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={9C42C00F-E104-4B86-A47F-F1F510D3AFC6}
License.Key={5A169522-249A-A937-25EB-4BA059C23DEE}
License.Name=Medialooks DXGI Screen Capture
License.UpdateExpirationDate=January 18, 2022
License.Edition=Standard
License.AllowedModule=*.*
License.Signature=8766050F279BD9169CE473D62BEEA242AD4971E093B47493713C4CE21BD4E810DAA3AF796CF73A71EAE6F6D42B6EF737DCE7022AE2157DE2D34D03C65A1D486B37DE7CF3E47A9550C3A147378DB904D260B099D7386A18D5D65DE2F4EE7A5BBBFE429FAE352E67BB36D84657442C4EF1932F98CE39B3BF8AEA4C0A368BBB2153

";

		//License initialization
        public static void IntializeProtection()
        {
            if (m_objMLProxy == null)
            {
                // Create MLProxy object 
                m_objMLProxy = new MLPROXYLib.CoMLProxyClass();
                m_objMLProxy.PutString(strLicInfo);                
            }
           UpdatePersonalProtection();
        }

        private static void UpdatePersonalProtection()
        {
            ////////////////////////////////////////////////////////////////////////
            // MediaLooks License secret key
            // Issued to: Planetcast Media Services Limited
            const long _Q1_ = 47419639;
            const long _P1_ = 33875999;
            const long _Q2_ = 58440869;
            const long _P2_ = 44344871;

            try
            {

                int nFirst = 0;
                int nSecond = 0;
                m_objMLProxy.GetData(out nFirst, out  nSecond);

                // Calculate First * Q1 mod P1
                long llFirst = (long)nFirst * _Q1_ % _P1_;
                // Calculate Second * Q2 mod P2
                long llSecond = (long)nSecond * _Q2_ % _P2_;

                uint uRes = SummBits((uint)(llFirst + llSecond));

                // Calculate check value
                long llCheck = (long)(nFirst - 29) * (nFirst - 23) % nSecond;
                // Calculate return value
                int nRand = new Random().Next(0x7FFF);
                int nValue = (int)llCheck + (int)nRand * (uRes > 0 ? 1 : -1);

                m_objMLProxy.SetData(nFirst, nSecond, (int)llCheck, nValue);

            }
            catch (System.Exception) { }

        }

        private static uint SummBits(uint _nValue)
        {
            uint nRes = 0;
            while (_nValue > 0)
            {
                nRes += (_nValue & 1);
                _nValue >>= 1;
            }

            return nRes % 2;
        }
    }