namespace Models
{
    public class Serial
    {
        string productSerialNumber;
        int uses;
        bool valid;

        public string ProductSerialNumber { get { return productSerialNumber; } set { productSerialNumber = value; } }
        public int Uses { get { return uses; } set { uses = value; } }
        public bool Valid { get { return valid; } set { valid = value; } }
    }
}
