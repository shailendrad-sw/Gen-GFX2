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
License.Key={D8C841EF-65EC-5F1A-97A9-67F7046EC039}
License.Name=MFile Module
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=CA2599C8DE7E9B0F084FEAD4CA4CC40C0F2B45A9364FAD9267A1F0A6944E37F4C73446B5B9BE9A1688F8F197E9C1830C85450AED83069FD747EB347B1C63E28F14BFBA7A3C7B0A64815C1C9A5762CF1EE8FA536DB1AE7F0EC1BC5AFF590E403C659638F718D51C1B53E33875F71B06911A47A587A6F1199A842EABF2691B1E52

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={519B7EAF-F067-4ECF-BDED-0E6C9DBCF3F3}
License.Key={6F42B433-D0B5-3D7C-AA6C-C88033645CB7}
License.Name=MPlaylist Module
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=84E9CBED988B3AECCA71F02E2DC686C5A50E26E92F7ED9C1906FA0299BC543C0F6E68D5A2B02D7A2CE146B134656ACCF52C1C4A7260C8FFC2E87D4214B757381ED5EE48A691E81361ED255384AEB613773524991902901D588070773139AE332B28C8F035F3E46E3E081E2C620660AF4E205CE4588E671E5962752172A9C5DBB

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={FF25E664-28C5-4123-B4BA-94A3481E96BE}
License.Key={EC0460F0-B34B-D6B8-78FF-FC9B363C83FE}
License.Name=MLive Module
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=D7695CBCF92987E0EBA281EE086FBC9353E874ED4DBB9A3E20D958C29BFA3309C7B5A712103296BF79CCE0BD66EED7F29B7884BE3897C20DA022C4D4299704F1AF7045B97B29A15B4D3D80B7D6044D7D56975431E60671F92A956FAF8FB260993015A1D3C28AED40709A7BBA4D878DCFDF6147325464CD96BB5BBF7AD8BB2E3A

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={B77ED627-4BE6-4D05-9528-5A0D78968814}
License.Key={4092406F-97B5-5BCE-8ABA-9E495A4A8526}
License.Name=MRenderer Module
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=80E87C77B34637A973A143E0ED5627AC525DD527E6FAC1CE395818C434EC52C6A1A8649847745F9D8A7D00C03553B9FD631B677EF1D5FADC8ED0E16AE76B05B498EA107BF84442FF47568B10A3B78197E70E06EF759F6F75ADB9D517FFA77E55ACFF55FAB86FEBB06DEA7B09A68DCD7984957854DB877755B8FFC85FE5BA3516

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={3051B1C8-1700-4B02-8015-9A94D8696798}
License.Key={4C010A45-26CE-64D9-3591-02DE0EF14EA5}
License.Name=MFormats Module
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=D32D2062B0ED9623D7244B880FE7F5AE82D41D7655CE672F524EBE742C98D4A128528DC1908089AD08481A0F7E8D018B271EC9BD6E09DDD00170628A0D1E62636E33E612B76B1D0814994F4B716B818B2A7CB41A03F0E85E37AEDD374BB3956F1DF4714C0B0287A13B8037E6FF99F214C53B1E6200859E8A9BB87A71CCFD8025

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={D9BED21B-1799-413E-BAD3-B6F74DB887B4}
License.Key={63DFF25D-334E-16F9-F942-191588B1A734}
License.Name=MFReader Module
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=4C224A5016F56FC6977EF38700F3FC03881E2D2602E5363D0A35C7167C10F6600E05996C813DABB081EC054AEB48993AD1C8FC7CC77203C7C281DDA8432750B18473A184D658558E42211DDE6E307ED663FD5588395C336F0D8F4687D7D828F7B7CD9B9457F0621E6767D83A7069F137AA2D80B976B76228713133543BEEA884

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={2B7F9BEC-9C3A-479C-9640-15705CF76938}
License.Key={FA15F3DD-E4E7-4868-79CE-8F85511D76A9}
License.Name=MFRenderer Module
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=F18D7E79508D24D54333709CB1EC4C60663196CDB5DA6C46C43605155403D84A504BB2336C180D543CD2212AC19EE0135A9F8C8177746FFA048476C6089F031C78280A51B48DE93D900DB22B8E68C28D57C9C3079F4BEB803E8EB73B51E7EB033F936A605A3E34886C8AF20BD39A592D2529DE33BCACB77710E6E259C110EC65

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={6D9538D0-4B0B-44FC-A823-318066626BB0}
License.Key={AE6A024D-1081-A7F3-AFC7-9EDA2816CD58}
License.Name=MFWriter Module
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=F47334F1C84A045FFBEF2BDE2A3286D22734226C5B668842DAC440DB13F31B92CD59544C4CCD0FC7552231BBFBE340EF4EC9AF9DA57024E676FA82EBE2C114E595108090EA0D1C713CB460168D3DA9BC6DB7C904B6A0BFC2050A95AD1503507BAC6A842CB023EED5D8CDF45494C88B70AAA1B0EF30DB7B6B84AE9455506F9DB3

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={58ACF52D-02C1-4946-B90C-DE694D235157}
License.Key={D636D413-27D1-4C46-D089-5CC32883EFF2}
License.Name=MTransportDS Module
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=46B6D27874AB4844F89E047F8BBA9AA8B5058A9ABB7DFDFE6CE37F38A78327BE62CEDB08AE9EE1D85CCBE86F0924C4E118001F04B6F5C683AB34FB83A9DB69F58E0ED3508E3AE3DD08BDD8588F2BE28904FEFF2E141F5C9C884847FD5943461252187C85D3B7A5D88791CD7A936EB0444731F9C321C30CF9C72624767A2D92BE

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={CEE37D3C-25E6-4F48-A1DB-A63211421434}
License.Key={F1708601-6A5D-203E-EC95-A29FD7FCA09B}
License.Name=MediaLooks Character Generator
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=2E7322C3E2D5846829A529B1868191B7958E94B77D60F2565C3E6EDE72310F9EE432E50D0C7D591D8CE57F3E63B16F421678CDF698622A53F1982806B98DEA5ACC231670D713394CB00AEDA5EE38A3030F2FF5AC6CF9F89242C181198FF37FD0645EA55FA79EB1FCBCB30584467D1F8A01D22CC17B4F60C3AA32268F8A337F75

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={20B18DBE-79D6-4BE9-90C2-460814A6000F}
License.Key={FA15F3DD-CEC9-34EE-CBC3-E5EB408D9E22}
License.Name=MMixer Module (BETA)
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=2936DD011F9DEE683B80C3634E543A89773339F2C6867903FE74AB96A30F71DC0092D87D11F3A093BF70AB9852B39E5CA3DDB7D5B63A0F94B000FD184C39454182F4B18D5369B0773F510AD7BCD33E6F42CE86C9B287E2058A6E47D1F2CFB21F82DB8EDBDC9A3B5F301AF38CFE4F7327C802B6CE759D574E71790D312BC69C86

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={B00D2E29-8341-4934-82D9-1E73196310A6}
License.Key={83D36F08-D16D-38B7-D378-308F8431BC18}
License.Name=MWriter Module
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=E9B2C32044406CA952F2079617FA3EC11D0CDE380CD7B86ED05FC1A93FAD9F6E31C51ECFB3B309975F700CDD76DFDE76B4E53A4F8D855A428FA6ACADC8685C5B541AE41B421D5E46BF67A4C85C5C73CCFD99C42C650F7C53647B48F49ABED6F7699A0D240A443DF449E42D10FB70301DE47B8C44CF6019793FB3367C6E991A9A

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={9F87A0A1-6D37-43D9-A21D-4C0E33608E88}
License.Key={2C0DADAA-DD12-F08D-23E5-BB8DE9BF250F}
License.Name=MPlatform Module
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=B6CA726EC2C4B68CC49B1A60E081B6E82C56A262D3B8C74400D5C3CA1E3F82C39587E38BEA726B34D270EC232E4A272339A899010A28F63D88D3795DB3FB79D01878F9331FD9D82816A18DC9C7F8EEC2F7848F0D57FB81C2478277C3FFBF9D9729530360D9B30D4F6A698D027C1881AED9078EDE70CAABD5A37857E0015136C7

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={694520ED-6E42-45EF-AC18-1BDF553E1C56}
License.Key={E9AB2AD6-C3A6-9FE2-EA78-C17DBBA675A8}
License.Name=MComposer Module
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=0BB5BE4280309312F6D00CFFF6334F709E16FF4A32696D2E0E43F73A4BB97262DEFD01BC4EBA3AA2AB59C819903EB18BB3D38BC685736535725FAB183932F6F67A8B23AFF74041F795354F21C5598AA5D2844D19FDBC3D6977573ED62E58A2567129A58F4888E6CBF06A2BD209166D869E322DA17E5493A9929648C32903858A

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={F6BB589A-A8D7-4462-9264-34BA2C1FC43D}
License.Key={FA15F3DD-33AB-E725-7EC4-1FF127B4D368}
License.Name=Delay internal module
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=1BAA43E767B0803FDF01AA3417FBA0C75CFFA28066830EEC436CF666B8EAD5DBD966258985B08CC574B28D041C74AE7221E35069C0D2D953778F91800249A5416315558C4BC992727C46F0AB3EB548EBC00BA354B46D9A08CA39B7B1ABCEB53D3FEBD15EFCCD4FC32BABFE5A1F0DF6475D931E8AAD9BB60FFE9B01A5D1AB2FC7

[MediaLooks]
License.ProductName=Closed Captions lib
License.IssuedTo=Planetcast Media Services Limited
License.CompanyID=287
License.UUID={5646E419-68EB-4F23-AA8D-45C06B815F2A}
License.Key={13A2C4A3-2BE5-2E41-FADE-CEA102DDB4D5}
License.Name=MWebRTC module
License.UpdateExpirationDate=October 21, 2021
License.Edition=Closed_Captions
License.AllowedModule=*.*
License.Signature=70DBFD76242C745C8B83E73F499FBDEFE57A9589235E93028AE671C4541799905692B1EB73C6DB73C5888CA85DA9C96A24F32B2C4302BDFE314A034B67DA49E5F1AD9864462DEDCBCF8633F5544282AEBDCDCCEB8AE922F78279F9D463C0986FA1FB56AC95F001C18B92969E77FE3FBDCED258E817ED2A34F2BE65A388EB5C67

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