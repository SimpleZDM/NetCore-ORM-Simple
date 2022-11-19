using MDT.VirtualSoftPlatform.Common;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class ProductCreateOrUpdateDto:IParamsVerify
    {

        public string DisplayName { get; set; }
        public string IconUrl { get; set; }
        public string Introduction { get; set; }

        public string UnityFile { get; set; }

        public int ComponentCount { get; set; }
    }
}
