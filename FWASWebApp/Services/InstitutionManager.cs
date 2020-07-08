using System.Collections.Generic;

namespace TGenWebApp.Services {
    public static class InstitutionManager {
        private static readonly Dictionary<string, Institution> Institutions  = new Dictionary<string, Institution>();

        public static void AddInstitution(string institutionId, string institutionName) {
            if (!Institutions.ContainsKey(institutionId)) {
                Institutions.Add(institutionId, new Institution(institutionId, institutionName));
            }
        }

        public static Institution GetInstitution(string institutionId) {
            return Institutions.ContainsKey(institutionId) ? Institutions[institutionId] : null;
        }
        
        // TODO: implement automatic worker to remove from cache
    }
}