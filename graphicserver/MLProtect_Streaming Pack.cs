//---------------------------------------------------------------------------
// MLProtect_StreamingPack.cs : Personal protection code for the Medialooks License system
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
// 3. Call StreamingPackLic.IntializeProtection() method before creating any Medialooks 
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

    public class StreamingPackLic
    {        
        private static MLPROXYLib.CoMLProxyClass m_objMLProxy;
        private static string strLicInfo = @"[MediaLooks]
License.ProductName=Streaming Pack
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={B00D2E29-8341-4934-82D9-1E73196310A6}
License.Key={A1EE5700-EA87-5BE8-B07E-BEBF4192B298}
License.Name=MWriter Module
License.UpdateExpirationDate=October 21, 2021
License.Edition=dvb
License.AllowedModule=*.*
License.Signature=3FD62F26DD61E8497D8EC86D4C153EA17399CFA2BB4C8419AE3E5D3F5375E8C6384461235167E859D333D4AA90A4622B63C99B2DF0A4BDA99C7D8111295A22B66CCCB66F6C6B239C4B19DE24630927F7C4E3F2B7A00491F37C1DAE2916FB8CD6FFF05B0AF1EA6C4519EBAD16A0BFF3E04640F55105FFA4F57ACDB3B034D09760

[MediaLooks]
License.ProductName=Streaming Pack
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={6D9538D0-4B0B-44FC-A823-318066626BB0}
License.Key={A1EE5700-0712-89B9-4832-3B5611CDCD54}
License.Name=MFWriter Module
License.UpdateExpirationDate=October 21, 2021
License.Edition=dvb
License.AllowedModule=*.*
License.Signature=4FB18B1CEDD9F4FB00F5797ABB5EE9A75B0DE802FC4A7C74BA4F52F0CFAD4696EC5B29424795D09681EDC640C1C3125A951F675F07E0145BF88835B898756AA89E42B6569C91BCADF41045E43251C2900E05DED8394C7741BF2DE9A90B397A09E28BD13C41C8A70456F418D09E83A0BDF2FB82ED907ECDF3D56A0D0E4071601F

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