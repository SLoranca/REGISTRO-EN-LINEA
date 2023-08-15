﻿using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Configuration;

namespace SEyTEvent.Models
{
    public class ModelEmail
    {
        public string asunto;
        public string body;
        public string html;
        string mode;
        
        public ModelEmail(string folio, string tblTalleres)
        {
            var urlQR = "";
            string path = "";
            string fileName = folio + ".jpg";
            string pathBase = HttpContext.Current.Server.MapPath("~/Content/qr");
            string urlGetQR = "";
            string modeForm = "";
           
            mode = WebConfigurationManager.AppSettings["Mode"].ToString();
            modeForm = WebConfigurationManager.AppSettings["Form"].ToString();

            if (mode == "DEV")
                //https://localhost:44306/
                urlQR = WebConfigurationManager.AppSettings["urlLocal"].ToString();
            else
                //http://sistemas.economiaytrabajo.chiapas.gob.mx/registro/
                urlQR = WebConfigurationManager.AppSettings["urlPublic"].ToString();

            asunto = "REGISTRO FORO DE INNOVACIÓN Y ECONOMÍA DIGITAL 2023";

            string url = urlQR + "SEFIED/RegistroExitoso?folio=" + folio;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());

                    bitMap.Save(pathBase +"\\" +fileName, ImageFormat.Jpeg);
                }
            }

            string[] path1 = { Directory.GetCurrentDirectory(), pathBase, fileName};
            path = System.IO.Path.Combine(path1);

            if (mode == "DEV")
            {
                urlGetQR = path; //solo para llenar la varible...
            }
            else
            {
                if (modeForm == "COMPLETO")
                    urlGetQR = "http://sistemas.economiaytrabajo.chiapas.gob.mx/registro/Content/qr/" + fileName;
                else
                    urlGetQR = "http://sistemas.economiaytrabajo.chiapas.gob.mx/registro_rapido/Content/qr/" + fileName;
            }
           
            html = "<!DOCTYPE html> <html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'> <head> <meta charset='UTF-8'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <meta name='viewport' content='width=device-width, initial-scale=1'> <title>Secretaría de Economía y del trabajo</title> <style type='text/css'> p { margin: 10px 0; padding: 0; } table { border-collapse: collapse; } h1, h2, h3, h4, h5, h6 { display: block; margin: 0; padding: 0; } img, a img { border: 0; height: auto; outline: none; text-decoration: none; } body, #bodyTable, #bodyCell { height: 100%; margin: 0; padding: 0; width: 100%; } .mcnPreviewText { display: none !important; } #outlook a { padding: 0; } img { -ms-interpolation-mode: bicubic; } table { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } p, a, li, td, blockquote { mso-line-height-rule: exactly; } a[href^=tel], a[href^=sms] { color: inherit; cursor: default; text-decoration: none; } p, a, li, td, body, table, blockquote { -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; } .ExternalClass, .ExternalClass p, .ExternalClass td, .ExternalClass div, .ExternalClass span, .ExternalClass font { line-height: 100%; } a[x-apple-data-detectors] { color: inherit !important; text-decoration: none !important; font-size: inherit !important; font-family: inherit !important; font-weight: inherit !important; line-height: inherit !important; } #bodyCell { padding: 10px; } .templateContainer { max-width: 600px !important; } a.mcnButton { display: block; } .mcnImage, .mcnRetinaImage { vertical-align: bottom; } .mcnTextContent { word-break: break-word; } .mcnTextContent img { height: auto !important; } .mcnDividerBlock { table-layout: fixed !important; } body, #bodyTable { background-color: #FAFAFA; } #bodyCell { border-top: 0; } .templateContainer { border: 0; } h1 { color: #202020; font-family: Helvetica; font-size: 26px; font-style: normal; font-weight: bold; line-height: 125%; letter-spacing: normal; text-align: left; } h2 { color: #202020; font-family: Helvetica; font-size: 22px; font-style: normal; font-weight: bold; line-height: 125%; letter-spacing: normal; text-align: left; } h3 { color: #202020; font-family: Helvetica; font-size: 20px; font-style: normal; font-weight: bold; line-height: 125%; letter-spacing: normal; text-align: left; } h4 { color: #202020; font-family: Helvetica; font-size: 18px; font-style: normal; font-weight: bold; line-height: 125%; letter-spacing: normal; text-align: left; } #templatePreheader { background-color: #FAFAFA; background-image: none; background-repeat: no-repeat; background-position: center; background-size: cover; border-top: 0; border-bottom: 0; padding-top: 9px; padding-bottom: 9px; } #templatePreheader .mcnTextContent, #templatePreheader .mcnTextContent p { color: #656565; font-family: Helvetica; font-size: 12px; line-height: 150%; text-align: left; } #templatePreheader .mcnTextContent a, #templatePreheader .mcnTextContent p a { color: #656565; font-weight: normal; text-decoration: underline; } #templateHeader { background-color: #FFFFFF; background-image: none; background-repeat: no-repeat; background-position: center; background-size: cover; border-top: 0; border-bottom: 0; padding-top: 9px; padding-bottom: 0; } #templateHeader .mcnTextContent, #templateHeader .mcnTextContent p { color: #202020; font-family: Helvetica; font-size: 16px; line-height: 150%; text-align: left; } #templateHeader .mcnTextContent a, #templateHeader .mcnTextContent p a { color: #007C89; font-weight: normal; text-decoration: underline; } #templateBody { background-color: #FFFFFF; background-image: none; background-repeat: no-repeat; background-position: center; background-size: cover; border-top: 0; border-bottom: 2px solid #EAEAEA; padding-top: 0; padding-bottom: 9px; } #templateBody .mcnTextContent, #templateBody .mcnTextContent p { color: #202020; font-family: Helvetica; font-size: 16px; line-height: 150%; text-align: left; } #templateBody .mcnTextContent a, #templateBody .mcnTextContent p a { color: #007C89; font-weight: normal; text-decoration: underline; } #templateFooter { background-color: #FAFAFA; background-image: none; background-repeat: no-repeat; background-position: center; background-size: cover; border-top: 0; border-bottom: 0; padding-top: 9px; padding-bottom: 9px; } #templateFooter .mcnTextContent, #templateFooter .mcnTextContent p { color: #656565; font-family: Helvetica; font-size: 12px; line-height: 150%; text-align: center; } #templateFooter .mcnTextContent a, #templateFooter .mcnTextContent p a { color: #656565; font-weight: normal; text-decoration: underline; } @media only screen and (min-width:768px) { .templateContainer { width: 600px !important; } } @media only screen and (max-width: 480px) { body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: none !important; } } @media only screen and (max-width: 480px) { body { width: 100% !important; min-width: 100% !important; } } @media only screen and (max-width: 480px) { .mcnRetinaImage { max-width: 100% !important; } } @media only screen and (max-width: 480px) { .mcnImage { width: 100% !important; } } @media only screen and (max-width: 480px) { .mcnCartContainer, .mcnCaptionTopContent, .mcnRecContentContainer, .mcnCaptionBottomContent, .mcnTextContentContainer, .mcnBoxedTextContentContainer, .mcnImageGroupContentContainer, .mcnCaptionLeftTextContentContainer, .mcnCaptionRightTextContentContainer, .mcnCaptionLeftImageContentContainer, .mcnCaptionRightImageContentContainer, .mcnImageCardLeftTextContentContainer, .mcnImageCardRightTextContentContainer, .mcnImageCardLeftImageContentContainer, .mcnImageCardRightImageContentContainer { max-width: 100% !important; width: 100% !important; } } @media only screen and (max-width: 480px) { .mcnBoxedTextContentContainer { min-width: 100% !important; } } @media only screen and (max-width: 480px) { .mcnImageGroupContent { padding: 9px !important; } } @media only screen and (max-width: 480px) { .mcnCaptionLeftContentOuter .mcnTextContent, .mcnCaptionRightContentOuter .mcnTextContent { padding-top: 9px !important; } } @media only screen and (max-width: 480px) { .mcnImageCardTopImageContent, .mcnCaptionBottomContent:last-child .mcnCaptionBottomImageContent, .mcnCaptionBlockInner .mcnCaptionTopContent:last-child .mcnTextContent { padding-top: 18px !important; } } @media only screen and (max-width: 480px) { .mcnImageCardBottomImageContent { padding-bottom: 9px !important; } } @media only screen and (max-width: 480px) { .mcnImageGroupBlockInner { padding-top: 0 !important; padding-bottom: 0 !important; } } @media only screen and (max-width: 480px) { .mcnImageGroupBlockOuter { padding-top: 9px !important; padding-bottom: 9px !important; } } @media only screen and (max-width: 480px) { .mcnTextContent, .mcnBoxedTextContentColumn { padding-right: 18px !important; padding-left: 18px !important; } } @media only screen and (max-width: 480px) { .mcnImageCardLeftImageContent, .mcnImageCardRightImageContent { padding-right: 18px !important; padding-bottom: 0 !important; padding-left: 18px !important; } } @media only screen and (max-width: 480px) { .mcpreview-image-uploader { display: none !important; width: 100% !important; } } @media only screen and (max-width: 480px) { h1 { font-size: 22px !important; line-height: 125% !important; } } @media only screen and (max-width: 480px) { h2 { font-size: 20px !important; line-height: 125% !important; } } @media only screen and (max-width: 480px) { h3 { font-size: 18px !important; line-height: 125% !important; } } @media only screen and (max-width: 480px) { h4 { font-size: 16px !important; line-height: 150% !important; } } @media only screen and (max-width: 480px) { .mcnBoxedTextContentContainer .mcnTextContent, .mcnBoxedTextContentContainer .mcnTextContent p { font-size: 14px !important; line-height: 150% !important; } } @media only screen and (max-width: 480px) { #templatePreheader { display: block !important; } } @media only screen and (max-width: 480px) { #templatePreheader .mcnTextContent, #templatePreheader .mcnTextContent p { font-size: 14px !important; line-height: 150% !important; } } @media only screen and (max-width: 480px) { #templateHeader .mcnTextContent, #templateHeader .mcnTextContent p { font-size: 16px !important; line-height: 150% !important; } } @media only screen and (max-width: 480px) { #templateBody .mcnTextContent, #templateBody .mcnTextContent p { font-size: 16px !important; line-height: 150% !important; } } @media only screen and (max-width: 480px) { #templateFooter .mcnTextContent, #templateFooter .mcnTextContent p { font-size: 14px !important; line-height: 150% !important; } } </style> </head> <body> <center> <table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable'> <tr> <td align='center' valign='top' id='bodyCell'> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='templateContainer'> <tr> <td valign='top' id='templatePreheader'> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnImageBlock' style='min-width:100%;'> <tbody class='mcnImageBlockOuter'> <tr> <td valign='top' style='padding:0px' class='mcnImageBlockInner'> <table align='left' width='100%' border='0' cellpadding='0' cellspacing='0' class='mcnImageContentContainer' style='min-width:100%;'> <tbody> <tr> <td class='mcnImageContent' valign='top' style='padding-right: 0px; padding-left: 0px; padding-top: 0; padding-bottom: 0; text-align:center;'> <img align='center' alt='' src='https://economiaytrabajo.chiapas.gob.mx/wp-content/uploads/2022/01/economia-color.jpeg' style='padding-bottom: 0px; vertical-align: bottom; display: inline !important; border: 1px none; max-width:170px; width:170px;' class='mcnImage'> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> <tr> <td valign='top' id='templateHeader'> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnImageCardBlock'> <tbody class='mcnImageCardBlockOuter'> <tr> <td class='mcnImageCardBlockInner' valign='top' style='padding-top:9px; padding-right:18px; padding-bottom:9px; padding-left:18px;'> <table align='right' border='0' cellpadding='0' cellspacing='0' class='mcnImageCardBottomContent' width='100%' style='background-color: #404040;'> <tbody> <tr> <td class='mcnImageCardBottomImageContent' align='left' valign='top' style='padding-top:0px; padding-right:0px; padding-bottom:0; padding-left:0px; background-color:#ffff;'> <div style='text-align:center'> <img alt='' src='" + urlGetQR + "' width='564' style='max-width:260px;' class='mcnImage'> </div> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> <tr> <td valign='top' id='templateBody'> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody> <tr> <td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'> <h1 style='text-align:center;'> Secretaría de Economía y del Trabajo </h1> <div style='text-align:center;'> <strong style='text-align:center; font-size:20px;'>¡FELICIDADES!</strong> </div> <p style='text-align:center;'> Su registro para participar en él evento Foro de Innovación y Economía Digital 2023 ha sido exitoso. </p> <p style='text-align:center; text-transform:uppercase; font-weight:bold;'> RECUERDA MOSTRAR ESTE QR EN EL EVENTO, SERVIRA PARA TU ACCESO </p> <hr> <p style='text-align:center;'>Talleres a los que estoy inscrito(a)</p> " + tblTalleres + "<p style='text-align:center;'>depto.competitividad@gmail.com </p> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> <tr> <td valign='top' id='templateFooter'> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnFollowBlock' style='min-width:100%;'> <tbody class='mcnFollowBlockOuter'> <tr> <td align='center' valign='top' style='padding:9px' class='mcnFollowBlockInner'> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnFollowContentContainer' style='min-width:100%;'> <tbody> <tr> <td align='center' style='padding-left:9px;padding-right:9px;'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width: 100%; border: 1px none;' class='mcnFollowContent'> <tbody> <tr> <td align='center' valign='top' style='padding-top:9px; padding-right:9px; padding-left:9px;'> <table align='center' border='0' cellpadding='0' cellspacing='0'> <tbody> <tr> <td align='center' valign='top'> <table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;'> <tbody> <tr> <td valign='top' style='padding-right:10px; padding-bottom:9px;' class='mcnFollowContentItemContainer'> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnFollowContentItem'> <tbody> <tr> <td align='left' valign='middle' style='padding-top:5px; padding-right:10px; padding-bottom:5px; padding-left:9px;'> <table align='left' border='0' cellpadding='0' cellspacing='0' width=''> <tbody> <tr> <td align='center' valign='middle' width='24' class='mcnFollowIconContent'> <a href='https://www.facebook.com/SEconomiayTrabajo' target='_blank'><img src='https://economiaytrabajo.chiapas.gob.mx/wp-content/uploads/2022/01/circle-fb.png' alt='Facebook' style='display:block;' height='24' width='24' class=''></a> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> <table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;'> <tbody> <tr> <td valign='top' style='padding-right:10px; padding-bottom:9px;' class='mcnFollowContentItemContainer'> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnFollowContentItem'> <tbody> <tr> <td align='left' valign='middle' style='padding-top:5px; padding-right:10px; padding-bottom:5px; padding-left:9px;'> <table align='left' border='0' cellpadding='0' cellspacing='0' width=''> <tbody> <tr> <td align='center' valign='middle' width='24' class='mcnFollowIconContent'> <a href='https://twitter.com/seytchiapas' target='_blank'><img src='https://economiaytrabajo.chiapas.gob.mx/wp-content/uploads/2022/01/twiter_logo_icon.png' alt='Instagram' style='display:block;' height='24' width='24' class=''></a> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> <table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;'> <tbody> <tr> <td valign='top' style='padding-right:0; padding-bottom:9px;' class='mcnFollowContentItemContainer'> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnFollowContentItem'> <tbody> <tr> <td align='left' valign='middle' style='padding-top:5px; padding-right:10px; padding-bottom:5px; padding-left:9px;'> <table align='left' border='0' cellpadding='0' cellspacing='0' width=''> <tbody> <tr> <td align='center' valign='middle' width='24' class='mcnFollowIconContent'> <a href='https://economiaytrabajo.chiapas.gob.mx/' target='_blank'><img src='https://economiaytrabajo.chiapas.gob.mx/wp-content/uploads/2022/01/circle-link.png' alt='Website' style='display:block;' height='24' width='24' class=''></a> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnBoxedTextBlock' style='min-width:100%;'> <tbody class='mcnBoxedTextBlockOuter'> <tr> <td valign='top' class='mcnBoxedTextBlockInner'> <table align='left' border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;' class='mcnBoxedTextContentContainer'> <tbody> <tr> <td style='padding-top:9px; padding-left:18px; padding-bottom:9px; padding-right:18px;'> <table border='0' cellspacing='0' class='mcnTextContentContainer' width='100%' style='min-width:100% !important;'> <tbody> <tr> <td valign='top' class='mcnTextContent' style='padding: 18px;color: #919191;font-family: Helvetica;font-size: 10px;font-weight: normal;text-align: center;'> <font style='vertical-align: inherit;'><font style='vertical-align: inherit;'><font style='vertical-align: inherit;'><font style='vertical-align: inherit;'>Por favor, piensa en el medio ambiente antes de imprimir este correo. El contenido de este mensaje de datos no se considera oferta, propuesta o acuerdo, los datos son confidenciales y para el uso exclusivo del destinatario, por lo que no podrá distribuirse y/o difundirse por ningún medio sin la previa autorización del emisor original.</font></font></font></font> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </table> </td> </tr> </table> </center> </body> </html>";

            body = html;
        }
    }
}