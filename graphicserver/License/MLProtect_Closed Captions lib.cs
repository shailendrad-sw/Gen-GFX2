//---------------------------------------------------------------------------
// MLProtect_ClosedCaptionslib.cs : Personal protection code for the Medialooks License system
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
// 3. Call ClosedCaptionslibLic.IntializeProtection() method before creating any Medialooks 
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

    public class ClosedCaptionslibLic
    {        
        private static MLPROXYLib.CoMLProxyClass m_objMLProxy;
        private static string strLicInfo = @"[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={A9019778-DA7C-4416-8054-3D3CB3106EF6}
License.Key={F5CDDC2B-9C52-33DA-700F-F37FDCE1C792}
License.Name=MFile Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=8D7DF94C3FD61E5E247E3B385253207B1E119CA898BE5B78EDF8CA0A9FA92B9D8C9CBC8873336814F29402D181377483AF8E413C1F1EF463FC56DB3AA66AA2344AA18FA91AEE7A191BD020E286ADA7134D6739EBB89C152C3F40195F563E4AE94E48931AF759CC4ECEA873BDDA22C23A6984A8E610A652D4DA722FD371CDB44D

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={519B7EAF-F067-4ECF-BDED-0E6C9DBCF3F3}
License.Key={FEE749E7-A4D9-A190-86B8-9C947BABB2F5}
License.Name=MPlaylist Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=F350FBE7BDBCE1B85D484FF77612629B0B6D99A3001BD26E168A7F26EEADD3B0ED0EE5A1D6E793E9BB40D75FD749AA5CB7E9A4408A858E8A14EB660715DD67BEF56D0CB58D3D5BD63FC82D287598133126EB461A42B42735852538F18D83BD54FF01B8CCA1B094658718A0DB861C16C63824A8F56482FDE4E7BA3891710EA076

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={FF25E664-28C5-4123-B4BA-94A3481E96BE}
License.Key={86ACA512-3A7E-13D0-B9AE-9336F09195F9}
License.Name=MLive Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=AB2D16BA5DEBFF584E9A5AF97B60E2EA64C505737D27FC563FCBCBDA639132CD39925A212400C85160F5568ECF10DECFD823DD483670397630C6339295F18D265CB283D46790314D96BDCF9243FF3D4B439F7BE7D1FDF4CFA2108A9E532A466DC478C5B27C9B1F799598EC64816001CC373E8FC3A9FC8C1CB5AF0D6A633B9350

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={B77ED627-4BE6-4D05-9528-5A0D78968814}
License.Key={13A2C4A3-C637-8092-30C9-1388F073737F}
License.Name=MRenderer Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=9F538CF8CA833B426D954A8FD00F6A6C6C180A34EAF4065889B2CCAA525F61449A7B905B09038273CA020959907AF5722D20098C5DA7FC9ADE31AD8CA47A6CA9D3A2CFC233BFA81050D5BF9435E0A0B74290974E8358B3A6BB20A4E35E2498FD49BBBE97DB25BDC0CB54CF0BD8DF5895885B3D8FF6782E689A7C94F4658EBE78

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={3051B1C8-1700-4B02-8015-9A94D8696798}
License.Key={B6DA7611-2F6C-2D9A-4C37-1A0EEFD26851}
License.Name=MFormats Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=A318BBD404E9F7CDA11A0070A0DBC117E513F98E870AAEA0A0FCE5BFB010B66D136AAC491C742905FF1A0EEB6122DA25F45644BED1B54D624BF21AFA07954DEDF3A7397A3A837F78C8DF74A1A0A3A7CF40E0EB7E93E6B80E278CF07824360B47110FCF920F9D147212A8A865304F172C0A2379187262FA78FE6340E2713E2405

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={D9BED21B-1799-413E-BAD3-B6F74DB887B4}
License.Key={18D18E99-7B2D-618D-38FB-1C6AEF6CE1D7}
License.Name=MFReader Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=745C731C828C7D35514D1C84867B7E1D4668849ED9A44A6BBC7288E2DF3BC976F773342AB9D96D19A4FF15C2ED7F3B5688E1B3EEE1F57D256BA2A05AC22FCDAE3F47B7B66F2675E93EF5B48397A091D9500093F9BB29795F26616BDA1D0FEE1030AF844C1DB9A5B4D19980902E2DA3F290D97B4592D610C5277092954537581F

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={2B7F9BEC-9C3A-479C-9640-15705CF76938}
License.Key={49B7CC3B-0428-63B1-C67D-F36CFB820220}
License.Name=MFRenderer Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=AAFBD106FCBAC934638E93C004AE6842E8329FEF8327D8DF3DAFE1A6C7DCF9659B5378E30E563F678EF4C491128499B7476899794398CC2D6B52632B705D2CB36FC883A970B2CE7378D60EC2FE0D64436BB9D66A3E26269AC4A5052860A0577C578C261D60DC2EF2685D717C108CC8E61A0D2B9B0AE3861DDA5079248C0D4992

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={6D9538D0-4B0B-44FC-A823-318066626BB0}
License.Key={D3A59E09-D69D-7066-6F23-6905FB17883E}
License.Name=MFWriter Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=B56CB2CC9C60C9FF313D684D3F197D97F6E23850472C26E3B2EF0A05838AB29F02B1D9590A5CC109771D23E0EE3D915006021E8DBA11E7A8DB4DD8BFE435DAA14249E1B06D78E1BE702270C59C2FC00510BA9D12C666776BE1B71D70CAFABAB56F2F5A09CDAD9F9A41E4E136DBA53A61E78D82F4567848D8B6CE6314D0874D20

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={58ACF52D-02C1-4946-B90C-DE694D235157}
License.Key={13A2C4A3-147E-5A13-FDA8-B0DC6A2E662C}
License.Name=MTransportDS Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=70FB8273D5C8D93A64F2131AF43DAC3479C19928D9A3A957777A58A4E70842CCABF0E6C47086A0E49597A9FB36E6E82571D31901868F299796E36F77E0B5FD4967240EB13D548D91AD3052E73DF9E468C793513B6EF661C0CBEF26508F0FCAA5EA12D1C66AE7FECB1673AF1BD9E4493520494C972C42B3A261DC004E6594AA9A

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={CEE37D3C-25E6-4F48-A1DB-A63211421434}
License.Key={13A2C4A3-63E3-25BB-FFA8-293FF979991B}
License.Name=MediaLooks Character Generator
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=9C92164C87260C9D5034882D27813DF8AFCDA13A21CFBE93A1B593CE27DD9B5F216BBA65E54AD335DC9479CCB2D9940CEC944AA9CA3698E86250CB3C69DE824B150810E2731A9B2A48551B29EDABEA5BF6379684A63F1462E009B43F1C84BF0AF3483EE5E5CA68DAC2E0DA95E0C8EDB998968BEB3D35A478B35528EF46FFE984

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={20B18DBE-79D6-4BE9-90C2-460814A6000F}
License.Key={5EE3D34C-A2EF-E960-8575-3D3E682D75AE}
License.Name=MMixer Module (BETA)
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=75E29C7099C726091FEE063CE811B494A62D8DDF8F460166E1E640787E2B916E75FC427F53F0285E55703C7EB3DAD1673DEC7CFCF59DEB594CB40EDC8E3D21A04D93D9E2EFF5FC68BC2D61256D43F5A07795AEEAFA08C975EE0D3458AD6BCA8A776D544A7A0A3BB5E4D70F7B6AC3DE3D1AA838DB62D311EA90503493A5463B35

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={B00D2E29-8341-4934-82D9-1E73196310A6}
License.Key={AE6A024D-97C3-81B4-0545-CB0E6E83D68B}
License.Name=MWriter Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=AA3B9493BCD02CD2EC12E3B95F982E450D05667C584E60446EA83B34DC61F62B2F92E25B4945005707167D50D31EE72DD2DFF77E6F254E2E4A5B49C345C63F1E84FCC000C3CAD5FD4CF0D4452285CEF2B63FEF4873391EAD9B65E4B184F67298D5F4866959AA5D463C3095D75C2CBE9C2888F9CC6F5724DDC94E7F9DABE711CB

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={9F87A0A1-6D37-43D9-A21D-4C0E33608E88}
License.Key={A1EE5700-481E-ACA0-73FA-A8C0BD5386D7}
License.Name=MPlatform Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=7D6E9A789CB1588B7063D73241022BDC33F7519CDB77526CF280BA3E2452C2D3AE334D9C2BD281DA3D60C0CEAD207D19EE286D2C0DE2F6A177ED49494562F514CA67B4B64804153452093831C1625C7331728FB1F6D2D6243273A53A201619A29DF9173C34331679AF4BAC2D1C4A26037C4DBD6BF8CECAF443811D5E334D79BF

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={694520ED-6E42-45EF-AC18-1BDF553E1C56}
License.Key={6F42B433-A5B6-23DB-1398-DE03168F46CB}
License.Name=MComposer Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=F6251D2142F7DEE7D356E0AAF74670F8FEA83B25E95D802EAE32954D6E9FC964CE111713CA8594B4D44AC5DF7625E24312BD2801A02F3D878ECD53D7F40E894B2B14C7F82DE75956D95CF87FE82F2E45FB7502772384853B9519372B293217D061489D364850E939969401122045DA34A03CC9B8200927677B01BEEED3C5331B

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={F6BB589A-A8D7-4462-9264-34BA2C1FC43D}
License.Key={8F4612EE-8679-6D35-301C-E21B5F8FB229}
License.Name=Delay internal module
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=7268D4E657ED08A4FCCE0118D177F0037E73FE00AF97332625DF8F96ACF132E15D523F7CF9FF8AA02A2D5BCDCFC7AC1DB49D9798A0832D7C47C00FDF421D9442BE152A04381F69E8CC6D7BE9599CC39DBED7745436F5390F7A71BAF993462E8D65EE706F90580EB7B52AC930CAED3007C2BC217695ACD7FD2D9F3C0A76FFB224

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={5646E419-68EB-4F23-AA8D-45C06B815F2A}
License.Key={0BE56F88-A6E2-138F-2CA5-2838ED292B8F}
License.Name=MWebRTC module
License.UpdateExpirationDate=November 21, 2026
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=2B0E2FC79C30CDFE05CEBA90818A7DF87971B626650D6A33D530A5D5713FBDBC070BB050EDB257DF44A7D58FEAD7EBBCA2D4D26BC4A3BC5D5116413AEA54735A84119063731CA7595BC6CE194472F476ED82D33D8D291A79A3EB33290E4928E2B07353BD55A1DA0B1D44683B0C469D1B1ABBC3C25E423E5C57CF9901F6F31149

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