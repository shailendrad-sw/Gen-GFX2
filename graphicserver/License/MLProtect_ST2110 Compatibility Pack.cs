//---------------------------------------------------------------------------
// MLProtect_ST2110CompatibilityPack.cs : Personal protection code for the Medialooks License system
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
// 3. Call ST2110CompatibilityPackLic.IntializeProtection() method before creating any Medialooks 
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

    public class ST2110CompatibilityPackLic
    {        
        private static MLPROXYLib.CoMLProxyClass m_objMLProxy;
        private static string strLicInfo = @"[MediaLooks]
License.ProductName=ST2110 Compatibility Pack
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={6317CD3C-696A-4C3E-A337-7B8BA8058E10}
License.Key={2C0DADAA-260B-245F-60D4-24B09486B8C4}
License.Name=Medialooks AJA2 module
License.UpdateExpirationDate=November 21, 2026
License.Edition=st2110
License.AllowedModule=*.*
License.Signature=3A4C0D40FE8FD3C2354ACF977C95C0481CC501188AD99F4FABF9B9390207BF26D7214E39F8EA3A3BBAAC9E5A56C815AB955DC3F8FA4E88824872D12D29E9A84912AEE75B2560630E137DC6E325F2F5A3479177A40C7B5A7B180726EE647AABA5FCAF39CD0060EC78697DE4589F4D9AD20A45A5565164DC0824C6AE906B145A31

[MediaLooks]
License.ProductName=ST2110 Compatibility Pack
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={D0FE6E83-7B4F-4187-B1C9-0C665BD6FFF4}
License.Key={FB62FB24-C058-4CD2-DDCD-7196E07816D0}
License.Name=Medialooks DeltaCast module
License.UpdateExpirationDate=November 21, 2026
License.Edition=st2110
License.AllowedModule=*.*
License.Signature=1A57CB7FD6368A98AAA5289CD2F88439CAB7D2E079DDE0F6BAC72C6D6F74E0751A524586340781398C2E12A4B5ACCC04481A6BA3ACF4FA36B2F05315D91E6EB0E51FDA635D8FDC006D34FCDFB1897D67FB745A8CB9BE3565CC0A7B430D44A1012D3DFE560EE67905F48B65ADFEB980F586BCB8D1CD67A3C0C446A1B265E4E61D

[MediaLooks]
License.ProductName=ST2110 Compatibility Pack
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={FF25E664-28C5-4123-B4BA-94A3481E96BE}
License.Key={B6337F78-57A7-9D8D-AC96-FC3B27EA9CD5}
License.Name=MLive Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=st2110
License.AllowedModule=*.*
License.Signature=30F10E5ED08DB744C29DDEAA59B5901282B575152D50FF49675DAD57FEF0F6915731BFC0890F48E304CFBFD0AA71F1DEEA0DA13D88347C56E6B397223FD4784910444DCE8EE6C32901C01FB96D46EA094E2607315D86DDBBF3EF6BF25E14B5BCD5ECBBB2ECAD6AA2470E364092C2A8B64BB4F42955E179FE4F543ECE042D8B63

[MediaLooks]
License.ProductName=ST2110 Compatibility Pack
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={B77ED627-4BE6-4D05-9528-5A0D78968814}
License.Key={2C0DADAA-A4A2-743B-E868-DDFCF76314A5}
License.Name=MRenderer Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=st2110
License.AllowedModule=*.*
License.Signature=7F4FC3342D85EC60FF5F695E20BD92F9093991FEACD648023765C0C386BDC3A0357726A7D95C51A78376F8D8CC69E932159614A18B5F7415CD5A27E702C23FA7884F58A0716B9ECC897F746199E83F59AC2579BD32B66141CE92D8802BD83383520DEC9B3FE6475FFB4F8DAD7296D31C92BE869727D87DA2867986DBED7F5B7B

[MediaLooks]
License.ProductName=ST2110 Compatibility Pack
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={3051B1C8-1700-4B02-8015-9A94D8696798}
License.Key={209EEBB4-6C8C-9760-2F9F-6BDC16B1FD1F}
License.Name=MFormats Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=st2110
License.AllowedModule=*.*
License.Signature=8D125E94F6D9DB2A6E1ECF7A31A036E2D04DE245410FA72DDF9486F87F62213EA753E2FCEC4A7B9F24DE4A8401C64412924191D307B9117BA0A2B204EAD2B2E101DE59E4E210D93B7B219A98144B53AB35CE79B8BD2A9A9A3CC0472FF8FA021168180606F6BC0E8E2C30841E6D5AEB59085FF813E978B7619783EF903218D635

[MediaLooks]
License.ProductName=ST2110 Compatibility Pack
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={2B7F9BEC-9C3A-479C-9640-15705CF76938}
License.Key={8F4612EE-9A74-03BA-F23E-FE79E4DF0D1E}
License.Name=MFRenderer Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=st2110
License.AllowedModule=*.*
License.Signature=A7DCA885C8DD6A9776A35CD6ADB3D1ABB49708F7DA30596E01D23114E2F0AB7E0235CB4185C4A1DE521152EFC3FF1C781F2FD4DE06F1874E8D6E143487351598C9185A68FBD0AAC35B06D9F28DF20D5F72AE168AB0879489FF77F79FF5EB54454C755E8091E5359D9933830F2471FF0F69B6EB4C40966D7C31058CB1096867B8

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