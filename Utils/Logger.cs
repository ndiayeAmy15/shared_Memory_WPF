using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WcfService1.Model;

namespace WcfService1.Utils
{
    public class Logger
    {
        BdSharedMemoryContext db = new BdSharedMemoryContext();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TitreErreur"></param>
        /// <param name="erreur"></param>
        public void WriteDataError(string TitreErreur, string erreur)
        {
            try
            {
                Td_Erreur log = new Td_Erreur();
                log.dateErreur = DateTime.Now;
                log.descriptionErreur = erreur.Length > 1000 ? erreur.Substring(0, 1000) : erreur;
                log.titreErreur = TitreErreur;
                db.td_erreurs.Add(log);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogSystem(ex.ToString(), "WriteDataError");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="erreur"></param>
        /// <param name="libelle"></param>
        public static void WriteLogSystem(string erreur, string libelle)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "App Vente Boutique";
                eventLog.WriteEntry(string.Format("date: {0}, libelle: {1}, description {2}", DateTime.Now, libelle, erreur), EventLogEntryType.Information, 101, 1);
            }
        }



    }
}