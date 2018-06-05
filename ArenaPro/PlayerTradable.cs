namespace ArenaPro.Domain
{
    public class PlayerTradable : Player
    {   
		public int Id { get; set; }
        public Club Club { get; set; }
        public decimal Price { get; set; }		
		
    }
}
