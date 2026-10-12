//---------------------------------------------------------------------------
// MLProtect_DVBStreamingLib.cs : Personal protection code for the Medialooks License system
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
// 3. Call DVBStreamingLibLic.IntializeProtection() method before creating any Medialooks 
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

    public class DVBStreamingLibLic
    {        
        private static MLPROXYLib.CoMLProxyClass m_objMLProxy;
        private static string strLicInfo = @"[MediaLooks]
License.ProductName=DVB Streaming Lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={B00D2E29-8341-4934-82D9-1E73196310A6}
License.Key={9D8B3946-C2AD-74D5-E938-6B0CAB0FDE93}
License.Name=MWriter Module
License.UpdateExpirationDate=October 11, 2019
License.Edition=dvb
License.AllowedModule=*.*
License.Signature=8897DC6CE3B9C12A76CBC74412AEB64649AE6C91DD101FD5F3EBFA2D34045386D6324658546472AEC21AC4BB6F2CB2ABDA8A64DFF50360EC9DC2308057433BBD14A50F3E35326FEEAEF0F0B893C6E5229A5036C5C1C787DAE11940BFE8DA2623D926E2005107E56983AEE88E471081B4BB4E372D1E42554401BF8A8361429677

[MediaLooks]
License.ProductName=DVB Streaming Lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={6D9538D0-4B0B-44FC-A823-318066626BB0}
License.Key={13A2C4A3-C58F-DBB3-EE75-63932E673B1B}
License.Name=MFWriter Module
License.UpdateExpirationDate=October 11, 2019
License.Edition=dvb
License.AllowedModule=*.*
License.Signature=688148D4AFB2B6FAE07419AFAE8BF92D782DF77E425A8DDA3E2E59E26703BF4ABC6EE4522F6A43012A4AF1B79AC8CCBF8D250FA62ED97BFD8B4FAC65CF5912CD5A408BA5E3739EB34BBB8C8572C2C6A558F15D973A7538837CFE3CBA3EFD5B8A377F60C447C03B360E630C3DB82883A23607DB99D87B76FEB63DF761533DF3CE

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