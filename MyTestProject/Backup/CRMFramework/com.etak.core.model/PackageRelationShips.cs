using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class PackageRelationShips
    {
        public int ID { get; set; }
        public RelationShips RelationShip { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is PackageRelationShips)
            {
                PackageRelationShips tmp = obj as PackageRelationShips;
                if (tmp.ID == this.ID &&
                    tmp.RelationShip.OriginPackage.PackageID == this.RelationShip.OriginPackage.PackageID &&
                    tmp.RelationShip.TargetPacakge.PackageID == this.RelationShip.TargetPacakge.PackageID)
                    return true;
                
                return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    [DataContract]
    [Serializable]
    public class RelationShips
    {
        public PackageInfo OriginPackage { get; set; }
        public PackageInfo TargetPacakge { get; set; }
      
    }

}
