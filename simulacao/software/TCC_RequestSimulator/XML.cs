using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC_Domain.Domain;
using System.Xml;

namespace TCC_RequestSimulator
{
    public static class XML
    {
        #region Taxista

        public static XmlDocument InitTaxistaIntoXML()
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmldoc.AppendChild(xmlnode);
            //let's add the root element

            XmlElement xml_taxistas = xmldoc.CreateElement("", "taxistas", "");
            xmldoc.AppendChild(xml_taxistas);

            return xmldoc;
        }

        public static XmlDocument TransformTaxistaIntoXML(XmlDocument xmldoc, Taxista taxista, int index)
        {
            //let's add another element (child of the root)
            XmlElement xml_taxista = xmldoc.CreateElement("taxista");
            xmldoc.ChildNodes.Item(1).AppendChild(xml_taxista);

            XmlElement xml_id = xmldoc.CreateElement("id");
            xml_id.AppendChild(xmldoc.CreateTextNode(taxista.ID.ToString()));
            xmldoc.ChildNodes.Item(1).ChildNodes.Item(index).AppendChild(xml_id);

            XmlElement xml_latitude = xmldoc.CreateElement("latitude");
            xml_latitude.AppendChild(xmldoc.CreateTextNode(taxista.PosicaoAtual.Latitude.ToString()));
            xmldoc.ChildNodes.Item(1).ChildNodes.Item(index).AppendChild(xml_latitude);

            XmlElement xml_longitude = xmldoc.CreateElement("longitude");
            xml_longitude.AppendChild(xmldoc.CreateTextNode(taxista.PosicaoAtual.Longitude.ToString()));
            xmldoc.ChildNodes.Item(1).ChildNodes.Item(index).AppendChild(xml_longitude);

            XmlElement xml_endereco = xmldoc.CreateElement("endereco");
            xml_endereco.AppendChild(xmldoc.CreateTextNode(taxista.PosicaoAtual.Endereco.ToString()));
            xmldoc.ChildNodes.Item(1).ChildNodes.Item(index).AppendChild(xml_endereco);

            XmlElement xml_status = xmldoc.CreateElement("status");
            xml_status.AppendChild(xmldoc.CreateTextNode(taxista.Status.ToString()));
            xmldoc.ChildNodes.Item(1).ChildNodes.Item(index).AppendChild(xml_status);

            return xmldoc;
        }        

        #endregion

        #region Cliente

        public static XmlDocument InitClienteIntoXML()
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmldoc.AppendChild(xmlnode);
            //let's add the root element

            XmlElement xml_taxistas = xmldoc.CreateElement("", "clientes", "");
            xmldoc.AppendChild(xml_taxistas);

            return xmldoc;
        }

        public static XmlDocument TransformClienteIntoXML(XmlDocument xmldoc, Cliente cliente, int index)
        {
            //let's add another element (child of the root)
            XmlElement xml_cliente = xmldoc.CreateElement("cliente");
            xmldoc.ChildNodes.Item(1).AppendChild(xml_cliente);

            XmlElement xml_id = xmldoc.CreateElement("id");
            xml_id.AppendChild(xmldoc.CreateTextNode(cliente.ID.ToString()));
            xmldoc.ChildNodes.Item(1).ChildNodes.Item(index).AppendChild(xml_id);

            XmlElement xml_latitude = xmldoc.CreateElement("latitude");
            xml_latitude.AppendChild(xmldoc.CreateTextNode(cliente.PosicaoAtual.Latitude.ToString()));
            xmldoc.ChildNodes.Item(1).ChildNodes.Item(index).AppendChild(xml_latitude);

            XmlElement xml_longitude = xmldoc.CreateElement("longitude");
            xml_longitude.AppendChild(xmldoc.CreateTextNode(cliente.PosicaoAtual.Longitude.ToString()));
            xmldoc.ChildNodes.Item(1).ChildNodes.Item(index).AppendChild(xml_longitude);

            XmlElement xml_endereco = xmldoc.CreateElement("endereco");
            xml_endereco.AppendChild(xmldoc.CreateTextNode(cliente.PosicaoAtual.Endereco.ToString()));
            xmldoc.ChildNodes.Item(1).ChildNodes.Item(index).AppendChild(xml_endereco);

            return xmldoc;
        }

        #endregion

        #region Matriz

        public static XmlDocument InitMatrizIntoXML()
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmldoc.AppendChild(xmlnode);
            //let's add the root element

            XmlElement xml_clientes = xmldoc.CreateElement("", "clientes", "");
            xmldoc.AppendChild(xml_clientes);

            return xmldoc;
        }

        public static XmlDocument TransformMatrizIntoXML(XmlDocument xmldoc, Cliente cliente, IList<Taxista> taxistas, int index)
        {
            //let's add another element (child of the root)
            XmlElement xml_cliente = xmldoc.CreateElement("cliente");
            xmldoc.ChildNodes.Item(1).AppendChild(xml_cliente);

            XmlNode node = xmldoc.ChildNodes.Item(1).ChildNodes.Item(index);

            #region Informações do cliente

            XmlElement xml_id = xmldoc.CreateElement("id");
            xml_id.AppendChild(xmldoc.CreateTextNode(cliente.ID.ToString()));
            node.AppendChild(xml_id);

            XmlElement xml_latitude = xmldoc.CreateElement("latitude");
            xml_latitude.AppendChild(xmldoc.CreateTextNode(cliente.PosicaoAtual.Latitude.ToString()));
            node.AppendChild(xml_latitude);

            XmlElement xml_longitude = xmldoc.CreateElement("longitude");
            xml_longitude.AppendChild(xmldoc.CreateTextNode(cliente.PosicaoAtual.Longitude.ToString()));
            node.AppendChild(xml_longitude);

            XmlElement xml_endereco = xmldoc.CreateElement("endereco");
            xml_endereco.AppendChild(xmldoc.CreateTextNode(cliente.PosicaoAtual.Endereco.ToString()));
            node.AppendChild(xml_endereco);

            #endregion

            #region Informações do Taxista

            XmlElement xml_taxistas = xmldoc.CreateElement("taxistas");
            xmldoc.ChildNodes.Item(1).ChildNodes.Item(index).AppendChild(xml_taxistas);

            for(int i=0; i<taxistas.Count; i++)
            {
                XmlNode node_taxistas = node.ChildNodes.Item(4);

                XmlElement xml_taxista = xmldoc.CreateElement("taxista");
                node_taxistas.AppendChild(xml_taxista);

                XmlNode node_taxista = node_taxistas.ChildNodes.Item(i);

                XmlElement xml_taxista_id = xmldoc.CreateElement("id");
                xml_taxista_id.AppendChild(xmldoc.CreateTextNode(taxistas[i].ID.ToString()));
                node_taxista.AppendChild(xml_taxista_id);

                XmlElement xml_taxista_latitude = xmldoc.CreateElement("latitude");
                xml_taxista_latitude.AppendChild(xmldoc.CreateTextNode(taxistas[i].PosicaoAtual.Latitude.ToString()));
                node_taxista.AppendChild(xml_taxista_latitude);

                XmlElement xml_taxista_longitude = xmldoc.CreateElement("longitude");
                xml_taxista_longitude.AppendChild(xmldoc.CreateTextNode(taxistas[i].PosicaoAtual.Longitude.ToString()));
                node_taxista.AppendChild(xml_taxista_longitude);

                XmlElement xml_taxista_endereco = xmldoc.CreateElement("endereco");
                xml_taxista_endereco.AppendChild(xmldoc.CreateTextNode(taxistas[i].PosicaoAtual.Endereco.ToString()));
                node_taxista.AppendChild(xml_taxista_endereco);

                XmlElement xml_taxista_status = xmldoc.CreateElement("status");
                xml_taxista_status.AppendChild(xmldoc.CreateTextNode(taxistas[i].Status.ToString()));
                node_taxista.AppendChild(xml_taxista_status);

                XmlElement xml_taxista_distancia = xmldoc.CreateElement("distancia");
                xml_taxista_distancia.AppendChild(xmldoc.CreateTextNode(string.Empty));
                node_taxista.AppendChild(xml_taxista_distancia);

                XmlElement xml_taxista_tempo = xmldoc.CreateElement("tempo");
                xml_taxista_tempo.AppendChild(xmldoc.CreateTextNode(string.Empty));
                node_taxista.AppendChild(xml_taxista_tempo);
            }

            #endregion

            return xmldoc;
        }

        #endregion

        public static void SaveIntoXML(XmlDocument xmldoc, string path)
        {
            try
            {
                xmldoc.Save(path); //I've chosen the c:\ for the resulting file pavel.xml
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
