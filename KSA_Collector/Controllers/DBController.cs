using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSA_Collector.Tables;

namespace KSA_Collector.Controllers
{
    internal class DBController
    {
        KSA_DBContext Context;
        public DBController(KSA_DBContext context)
        {
            Context = context;
        }

        internal void SaveSessionEcuIdentification(Tables.SessionEcuidentification sessionEcuidentification)
        {
            Tables.SessionEcuidentification tmp = Context.SessionEcuidentifications.Where(c => c.IdSession == sessionEcuidentification.IdSession && c.IdEcuidentifications == sessionEcuidentification.IdEcuidentifications).SingleOrDefault();

            if (tmp == null)
            {
                tmp = sessionEcuidentification;
                Context.SessionEcuidentifications.Add(tmp);
                Context.SaveChanges();
            }
        }

        internal void SaveComposite(Tables.Composite composite)
        {
            Tables.Composite tmp = Context.Composites.Where(c => c.DesignNumber == composite.DesignNumber && c.IdEcuNavigation == composite.IdEcuNavigation).SingleOrDefault();

            if (tmp == null)
            {
                tmp = composite;
                Context.Composites.Add(tmp);
                Context.SaveChanges();
            }
        }

        internal void SaveAoglonassReport(Tables.AoglonassReport aoglonassReport)
        {
            Tables.AoglonassReport tmp = Context.AoglonassReports.Where(c => c.IdSession == aoglonassReport.IdSession && c.DateStart == aoglonassReport.DateStart).SingleOrDefault();

            if (tmp == null)
            {
                tmp = aoglonassReport;
                Context.AoglonassReports.Add(tmp);
                Context.SaveChanges();
            }
        }

        internal void SaveProcedureReport(Tables.ProcedureReport procedureReport)
        {
            Tables.ProcedureReport tmp = Context.ProcedureReports.Where(c => c.IdSession == procedureReport.IdSession).SingleOrDefault();

            if (tmp == null)
            {
                tmp = procedureReport;
                Context.ProcedureReports.Add(tmp);
                Context.SaveChanges();
            }
        }

        internal void SaveSessionEcu(Tables.SessionEcuidentification sessionEcu)
        {
            Tables.SessionEcuidentification tmp = Context.SessionEcuidentifications.Where(c => c.IdSessionNavigation == sessionEcu.IdSessionNavigation && c.IdEcuidentificationsNavigation == sessionEcu.IdEcuidentificationsNavigation).SingleOrDefault();

            if (tmp != null)
            {
                tmp = sessionEcu;
                Context.SessionEcuidentifications.Add(sessionEcu);
                Context.SaveChanges();
            }
        }

        internal Tables.System GetSystem(Tables.System system)
        {
            Tables.System tmp = Context.Systems.Where(c => c.Name == system.Name && c.Domain == system.Domain).SingleOrDefault();

            if (tmp != null)
                return tmp;
            else
                return system;
        }

        internal Tables.Ecu GetECU(Tables.Ecu ecu)
        {
            Tables.Ecu tmp = Context.Ecus.Where(c => c.Codifier == ecu.Codifier).SingleOrDefault();

            if (tmp != null)
                return tmp;
            else
                return ecu;
        }

        internal Tables.Identification GetIdentification(Tables.Identification identification)
        {
            Tables.Identification tmp = Context.Identifications.Where(c => c.Name == identification.Name && c.Value == identification.Value).SingleOrDefault();

            if (tmp != null)
                return tmp;
            else
                return identification;
        }

        internal Tables.EcuIdentification GetEcuIdentification(Tables.EcuIdentification ecuIdentification)
        {
            Tables.EcuIdentification tmp = Context.EcuIdentifications.Where(c => c.IdEcu == ecuIdentification.IdEcu && c.IdIdentifications == ecuIdentification.IdIdentifications).SingleOrDefault();

            if (tmp != null)
                return tmp;
            else
                return ecuIdentification;
        }

        internal Tables.Session GetSession(Tables.Session session)
        {
            Tables.Session tmp = Context.Sessions.Where(c => c.SessionsName == session.SessionsName).SingleOrDefault();

            if (tmp != null)
                return tmp;
            else
                return session;
        }

        internal Tables.Vehicle GetVehicle(Tables.Vehicle vehicle)
        {
            Tables.Vehicle tmp = Context.Vehicles.Where(c => c.Vin == vehicle.Vin && c.Iccid == vehicle.Iccid && c.Iccidc == vehicle.Iccidc && c.Imei == vehicle.Imei && c.Type == vehicle.Type).SingleOrDefault();

            if (tmp != null)
                return tmp;
            else
                return vehicle;
        }

        internal Tables.ServiceCenter GetServiceCenter(string username)
        {
            return Context.ServiceCenters.Where(c => c.Username == username).Single();
        }

        internal void SaveServiceCenter(Tables.ServiceCenter[] serviceCenters)
        {
            Context.ServiceCenters.AddRange(serviceCenters);
            Context.SaveChanges();
        }
    }
}
