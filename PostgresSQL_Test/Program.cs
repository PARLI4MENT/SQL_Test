﻿#define TEST 

using System.Diagnostics;
using System.Xml;
using SqlTest;

namespace MainNs
{
    public class Program
    {

        public static void Main(string[] args)
        {
            /// Dont use
            #region
            //{
            //    var sw = new Stopwatch();
            //    sw.Restart();

            //    var thread = new Thread(() =>
            //    {
            //        PgInsertData("testTabel1", 0, 10000);
            //    });

            //    thread.Start();
            //    thread.Join();

            //    sw.Stop();
            //    Console.WriteLine(sw.Elapsed);
            //}
            #endregion

            /// Dont use
            #region
            //{
            //    var sw = new Stopwatch();
            //    sw.Start();
            //    sw.Restart();

            //    Task th = Task.Factory.StartNew(() => PgInsertData("testTabel1", 10000));
            //    Task.WhenAll(th).ContinueWith(task => sw.Stop());

            //    Console.WriteLine(sw.Elapsed);

            //}
            #endregion

            //{
            //    string[] pathsFiles = Directory.GetFiles("C:\\_test\\ParseOutput");

            //    foreach (var filePath in pathsFiles)
            //    {
            //        ParseXML(filePath);
            //    }
            //}

            {
                ParseXML("C:\\_test\\_test\\1001204E\\00251779-b785-4cc1-92f9-8690174f14fa.92ec414c-ce5f-4cb8-a81c-cb62bcb60780.Passport.xml");
            }

            //#region Parse
            //{
            //    var renamer = new RenamerXML();
            //    renamer.ParseFileByMaskedParallel();
            //}
            //#endregion

            Console.ReadKey();
        }

        private static void ParseXML(string FileName)
        {
            try
            {
                string FileInFolder = "C:\\_test\\_test\\DOCS_TO_ARCH";
                string FileOutFolder = "C:\\_test\\_test\\OUT";
                const string FileTemplate = "C:\\_test\\create_doc_in_arch.xml";

                //var sw = new Stopwatch();
                //sw.Start();

                const int Company_key_id = 1;

                string DateStr = FileName + ";";

                //File.AppendAllText("C:\\_test\\Arch_docs.log", Environment.NewLine + "New TEST;START;END CASE;PREP XML;SING XML;INSERT;");

                // Сделать String.Split
                // Переделать, забирает только первую часть [0]
                string NameArray = (string)Path.GetFileName(FileName).Split('.')[0];
                var file_xml = new XmlDocument();
                var doc_to_arch = new XmlDocument();

                string? PrDocumentName = "", PrDocumentNumber = "", PrDocumentDate = "", DocCode = "", DocName;

                string NewDocToArchName = Path.Combine(FileInFolder, Path.GetFileName(FileName));
                File.Copy(FileTemplate, NewDocToArchName, true);

                /// Ошибка Unicode
                //file_xml.Load(pathToXmlFile);
                file_xml.Load(new StringReader(File.ReadAllText(FileName)));
                switch (file_xml.DocumentElement.GetAttribute("DocumentModeID"))
                {
                    //'Договор ТамПред
                    case "1006196E":
                        /*
                            PrDocumentName = DirectCast(file_xml.GetElementsByTagName("ContractDetails", "*")(0), XmlElement).GetElementsByTagName("PrDocumentName", "*")(0).InnerText
                            PrDocumentNumber = DirectCast(file_xml.GetElementsByTagName("ContractDetails", "*")(0), XmlElement).GetElementsByTagName("PrDocumentNumber", "*")(0).InnerText
                            PrDocumentDate = DirectCast(file_xml.GetElementsByTagName("ContractDetails", "*")(0), XmlElement).GetElementsByTagName("PrDocumentDate", "*")(0).InnerText
                            DocCode = "11002"
                            DocName = "Договор с ТамПред"
                         */
                        {
                            /// НУ ХЗ ХЗ
#if TEST
                            PrDocumentName = ((XmlElement)file_xml.GetElementsByTagName("ContractDetails", "*")[0]).GetElementsByTagName("PrDocumentName", "*")[0].InnerText;
                            PrDocumentNumber = ((XmlElement)file_xml.GetElementsByTagName("ContractDetails", "*")[0]).GetElementsByTagName("PrDocumentNumber", "*")[0].InnerText;
                            PrDocumentDate = ((XmlElement)file_xml.GetElementsByTagName("ContractDetails", "*")[0]).GetElementsByTagName("PrDocumentDate", "*")[0].InnerText;
                            DocCode = "11002";
                            DocName = "Договор с ТамПред";
#endif
                        }
                        break;

                    //Довереность
                    case "1002008E":
                        {
                            PrDocumentName = file_xml.GetElementsByTagName("PrDocumentName")[0].InnerText;
                            PrDocumentNumber = file_xml.GetElementsByTagName("PrDocumentNumber")[0].InnerText;
                            PrDocumentDate = file_xml.GetElementsByTagName("PrDocumentDate")[0].InnerText;
                            DocCode = "11003";
                            DocName = "Доверенность";
                        }
                        break;

                    //Паспорт
                    case "1001204E":
                        {
                            PrDocumentName = "Паспорт гражданина РФ";
                            PrDocumentNumber = $"{file_xml.GetElementsByTagName("CardSeries")[0].InnerText}" +
                                $" {file_xml.GetElementsByTagName("CardNumber")[0].InnerText}";
                            PrDocumentDate = file_xml.GetElementsByTagName("CardDate")[0].InnerText;
                            DocCode = "11001";
                            DocName = "Паспорт декларанта";
                        }
                        break;

                    //Индивидуальная
                    case "1002018E":
                        {
                            PrDocumentName = "Индивидуальная накладная при экспресс перевозке";
                            PrDocumentNumber = file_xml.GetElementsByTagName("WayBillNumber")[0].InnerText;
                            if (file_xml.GetElementsByTagName("DateTime").Count > 0)
                                PrDocumentDate = file_xml.GetElementsByTagName("DateTime")[0].InnerText;
                            else
                                PrDocumentDate = "";
                            DocCode = "02021";
                            DocName = "Индивидуальная накладная";
                            file_xml.GetElementsByTagName("InternationalDistribution")[0].InnerText = "1";
                        }
                        break;

                    //Графические материалы
                    case "1006110E":
                        {
                            PrDocumentName = file_xml.GetElementsByTagName("PrDocumentName")[0].InnerText;
                            PrDocumentNumber = file_xml.GetElementsByTagName("PrDocumentNumber")[0].InnerText;
                            PrDocumentDate = file_xml.GetElementsByTagName("PrDocumentDate")[0].InnerText;
                            DocCode = "09023";
                            DocName = $"Графические материалы: {PrDocumentName} {PrDocumentNumber}";
                            //file_xml.GetElementsByTagName("FileData")(0).InnerText 
                            //    = Replace(file_xml.GetElementsByTagName("FileData")(0).InnerText, Chr(13), "")
                            file_xml.GetElementsByTagName("FileData")[0].InnerText =
                                file_xml.GetElementsByTagName("FileData")[0].InnerText.Replace("\r", "");
                        }
                        break;

                    //Текстовый документ
                    // Нет документа для теста
                    case "1006088E":
                        {
                            PrDocumentName = file_xml.GetElementsByTagName("DocumentName")[0].InnerText;
                            PrDocumentNumber = file_xml.GetElementsByTagName("DocumentNumber")[0].InnerText;
                            if (file_xml.GetElementsByTagName("DocumentDate").Count > 0)
                                if (PrDocumentName == "Экспертиза")
                                    DocCode = "10023";
                                else
                                    DocCode = "09999";
                            DocName = PrDocumentName;
                        }
                        break;

                    //Инвойс
                    case "1002007E":
                        {
                            PrDocumentName = "Инвойс";
                            PrDocumentNumber = file_xml.GetElementsByTagName("PrDocumentNumber", "*")[0].InnerText;
                            if (file_xml.GetElementsByTagName("PrDocumentDate", "*").Count > 0)
                                PrDocumentDate = file_xml.GetElementsByTagName("PrDocumentDate", "*")[0].InnerText;
                            DocCode = "04021";
                            DocName = PrDocumentName;
                        }
                        break;

                    //Расчет Утиль Сбора
                    // Нет документа для теста
                    case "1002048E":
                        {
                            PrDocumentName = "Расчет утилизационного сбора";
                            PrDocumentNumber = "";
                            PrDocumentDate = file_xml.GetElementsByTagName("CalculateDate", "*")[0].InnerText;
                            DocCode = "10064";
                            DocName = PrDocumentName;
                        }
                        break;

                    //Коносамент
                    // Нет документа для теста
                    case "1003202E":
                        {
                            PrDocumentName = "Коносамент";
                            PrDocumentNumber = file_xml.GetElementsByTagName("PrDocumentNumber", "*")[0].InnerText;
                            PrDocumentDate = file_xml.GetElementsByTagName("PrDocumentDate")[0].InnerText;
                            DocCode = "02011";
                            DocName = PrDocumentName;
                        }
                        break;
                }

                doc_to_arch.Load(NewDocToArchName);

                //Dim temp_node As XmlNode = doc_to_arch.ImportNode(file_xml.DocumentElement, True)
                //doc_to_arch.GetElementsByTagName("Object")(1).AppendChild(temp_node)
                var temp_node = doc_to_arch.ImportNode(file_xml.DocumentElement, true);
                doc_to_arch.GetElementsByTagName("Object")[1].AppendChild(temp_node);

                string EnvelopeID = Guid.NewGuid().ToString().ToUpper();
                string DocumentID = Guid.NewGuid().ToString().ToUpper();

                doc_to_arch.GetElementsByTagName("EnvelopeID", "*")[0].InnerText = EnvelopeID;
                doc_to_arch.GetElementsByTagName("roi:SenderInformation")[0].InnerText = "SenderInformation_TEMP";
                doc_to_arch.GetElementsByTagName("roi:PreparationDateTime")[0].InnerText = DateTime.Now.ToString("s") + DateTime.Now.ToString("zzz");
                doc_to_arch.GetElementsByTagName("ParticipantID")[0].InnerText = "ParticipantID";
                doc_to_arch.GetElementsByTagName("CustomsCode")[0].InnerText = "10000000";
                doc_to_arch.GetElementsByTagName("X509Certificate")[0].InnerText = "X509Certificate_TEMP";
                doc_to_arch.GetElementsByTagName("ct:DocumentID")[0].InnerText = (Guid.NewGuid().ToString()).ToUpper();
                doc_to_arch.GetElementsByTagName("ct:ArchDeclID")[0].InnerText = "ArchDeclID_TEMP";
                doc_to_arch.GetElementsByTagName("ct:ArchID")[0].InnerText = "ArchID_TEMP";
                doc_to_arch.GetElementsByTagName("DocumentID", "*")[1].InnerText = DocumentID;
                doc_to_arch.GetElementsByTagName("DocCode", "*")[0].InnerText = DocCode;
                doc_to_arch.GetElementsByTagName("X509Certificate", "*")[0].InnerText = "X509Certificate_TEMP *";

                ((XmlElement)doc_to_arch.GetElementsByTagName("DocBaseInfo", "*")[0]).GetElementsByTagName("PrDocumentName", "*")[0].InnerText = PrDocumentName;

                if (string.IsNullOrEmpty(PrDocumentNumber))
                    doc_to_arch.GetElementsByTagName("DocBaseInfo", "*")[0].RemoveChild(((XmlElement)doc_to_arch.GetElementsByTagName("DocBaseInfo", "*")[0]).GetElementsByTagName("PrDocumentNumber", "*")[0]);
                else
                    ((XmlElement)doc_to_arch.GetElementsByTagName("DocBaseInfo", "*")[0]).GetElementsByTagName("PrDocumentNumber", "*")[0].InnerText = PrDocumentNumber;

                if (string.IsNullOrEmpty(PrDocumentDate))
                    doc_to_arch.GetElementsByTagName("DocBaseInfo", "*")[0].RemoveChild(((XmlElement)doc_to_arch.GetElementsByTagName("DocBaseInfo", "*")[0]).GetElementsByTagName("PrDocumentDate", "*")[0]);
                else
                    ((XmlElement)doc_to_arch.GetElementsByTagName("DocBaseInfo", "*")[0]).GetElementsByTagName("PrDocumentDate", "*")[0].InnerText = PrDocumentDate;

                doc_to_arch.Save(NewDocToArchName);

                //SignerXMLFile(NewDocToArchName, NewDocToArchName2, Company_key_id);
                File.Copy(NewDocToArchName, Path.Combine(FileOutFolder, Path.GetFileName(FileName)), true);

                File.AppendAllText("C:\\_test\\Arch_docs.log", "New TEST;START;END CASE;PREP XML;SING XML;INSERT;");

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return; }
        }


        private static void SignerXMLFile(string newDocToArchName, string newDocToArchName2, int company_key_id)
        {
        }

        private static void CopyFile(string pathFile, string str = "")
        {
            File.Copy(pathFile, Path.Combine("C:\\_test\\_test", String.Concat(str, Path.GetFileName(pathFile))));
        }
    }
}