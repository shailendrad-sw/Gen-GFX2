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
License.Key={35CB0AC5-004B-4F3F-82C4-2560EC0B0E0B}
License.Name=MWriter Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=dvb
License.AllowedModule=*.*
License.Signature=51B4FF4BB8F5132DF12BBD81DE87684DEC07ABECEDDA8A78C5D44217B653B7381D159DCA8A5B1D30D71C4804CED090F47ABEC3597742B0235FA6063D3993E1D1028257142D0D9DD0E5F498C2D985FE6B222405E4B7F5F3EFE4E8908D36C823011320A9A590342DA25143E826D2FC2EC4F3B9EC4C0798527E4FB0E4A006B3B32D

[MediaLooks]
License.ProductName=Streaming Pack
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={6D9538D0-4B0B-44FC-A823-318066626BB0}
License.Key={13A2C4A3-147E-5A13-7B6A-C977ED9C611C}
License.Name=MFWriter Module
License.UpdateExpirationDate=November 21, 2026
License.Edition=dvb
License.AllowedModule=*.*
License.Signature=5571E441E960A77922C56F7226F1B9DDFEFF3CDFB6CDFF55CD05ACB06A800364DB93AC1724334A447B7AACA0A2F6B233273A46549E095E27FC500423D01326C0EC6722FD49E5BAD98DB9C7BAA471387D8A12396A7C46C9E3B1696AA1EC95DA2234276F9E9BB3920675541B3F8400C31964895200076BC519067EC77453451A8B

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