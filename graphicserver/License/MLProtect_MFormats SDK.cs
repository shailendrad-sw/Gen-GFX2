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
License.Key={9CB231FF-681F-22A5-FCE1-D390D3B80056}
License.Name=MFormats Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=8127E40CAEEB7150BF9C6F81077547E9287AD418B2BEB8637E51EE5E228D904062178960FF07593268884D389FABA1388DF84FA834AFF8C4E27C689D81DA96DE1D51D392AF3EDD32C02F642FD0CA99D92B3336DCE1EA4C6CA54CA4956142A7E1DD4E547196B8FF02937428E46B3385E96F56133C3A0E374E5020269DF84605C6

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={2B7F9BEC-9C3A-479C-9640-15705CF76938}
License.Key={66A94867-EEB4-D817-8A9C-FBB76D65F9D3}
License.Name=MFRenderer Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=064AC2C14FF2FE565D7CAF71D16B32506ADE423EADA077B34241D9F98E42B435EBED1039359A16DFF8374D9D1C60984F0289D23CE8805B652F69F80FF9297078EF9BECB8FC31F7F47975A72E619E607534DBE51D9E186579BF7634B005F5A3C7CD3549E4F9485240FD9BB4AA219BEC645A19095E72566D828C8F5CC1200AB607

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={D9BED21B-1799-413E-BAD3-B6F74DB887B4}
License.Key={4C010A45-C865-2055-B0D8-381DE07052B8}
License.Name=MFReader Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=8D2A49FE74A2A5157CBAF80D6F6C92BB925FD79E2B1D29BB7FB8C827AABE362D1AB8A6F5E3096237318125E8001DFCEEFF919802FEB9D99E94F9E3681E50819B6A9127CD2696BCF5953CEB89B7AEA113B8310CCFBF38C1E2C6852E6BA095CF3E2CE079C2F5FCC6E3657324DEFF0C21074D6D78A5E7E20163D5D01D892608258F

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={6D9538D0-4B0B-44FC-A823-318066626BB0}
License.Key={FEE749E7-4FEB-0105-A552-1535E53BADE7}
License.Name=MFWriter Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=8C7F8D91D512173DAAD295D0A3389DB599C822D45E4BC1B72C8B6901E1C83FD4EB1781197C9E0A2514EAD9068825E08A7B5E382C4B480E24B533E88E2093C5AFE8AA2A70C6C5ED10B9AE58A685F91203347C1704D017E1729C04C6488DE8C4ADD198202F3ABF54C85B60A5DBCD7D8640EAA37615D2A18ED5708378ED88A7F61A

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={F6BB589A-A8D7-4462-9264-34BA2C1FC43D}
License.Key={2781E26D-85E5-C700-5194-728B8A3766C9}
License.Name=Delay internal module
License.UpdateExpirationDate=November 21, 2026
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=285E325759D776772704648E5EFFBE92AD908DA3036E4177E88B81943523EB2B0D4950AF48214057B63DB2FEA7D3F67FE5115188CE0B1F774088F0955598509D920E6F136F89E118C20E7C0C0528665A8C5B7FBBB339D1156292DFCA94ED088633EBF530C4DB26BE1F5FC85A81A58436EFF680C1F305075DCEEBA18F6AAD2C85

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={5646E419-68EB-4F23-AA8D-45C06B815F2A}
License.Key={8F4612EE-F057-767F-1F74-88A686AFEDDF}
License.Name=MWebRTC module
License.UpdateExpirationDate=November 21, 2026
License.Edition=Standard gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=9D0522250AC2ECE6C53E400012E5B73AB9B95CD90C3FD5E895A2007E99F228616FA337D29D5F5842FBCA5CC0CDB44866DDC13EDB5495D31E73C7DB03EC641370A0E0D559C6A73764186E713526A05369BEA744C17200E93CEEBBCE6D513A30CF909650033F4015563DA597298FB24DD0D59C98802D809955E4C9B5D2F7804850

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={9C42C00F-E104-4B86-A47F-F1F510D3AFC6}
License.Key={A2C75857-0269-4C0D-6219-3EA766D6F2B7}
License.Name=Medialooks DXGI Screen Capture
License.UpdateExpirationDate=November 21, 2026
License.Edition=Standard
License.AllowedModule=*.*
License.Signature=EFA22F0CEA438437C4BCF458180E4BFFE8EDE230288F1AC7EA8E3E9B0140F903A359644166853BAC9190146B0ABE89AE39F00D19B1C361E9F5902678E0A5B32178917DA6634795776FCB8096F85D5DA65290036D7FAAE53EFE4F180892E0C3C379876E2BB4F03D7A17F157FD94E0292A112B305866E1C038A70667E2EAA989C2

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={A6886FFF-447A-484D-97C6-0AABC56933E4}
License.Key={8F4612EE-F55E-3192-3696-51BC881BCC04}
License.Name=MFormats Codecs Library core
License.UpdateExpirationDate=November 21, 2026
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=0D9D60C6F0CC60D5C92D3601D68E9A1B251B3F438BE353D2AF2103DFD05ECE28491F3B3818161B4309B62735B1372B3F11C1B06F6B5BAB38FEAA1621E4A9DA7C05459BAFFDE24D80799B4C91629688E437D330D3D5E34B214DBA2C423E4A846C568DEB671390DADB3AED004229D548CDF6DA0A624F46B4E97E733BB1D520B941

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