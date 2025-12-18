namespace cs_prog_17
{
    public class Player : Character
    {
        private int _healAmount;
        private double _specialAttackMultiplier;
        private bool _specialAttackUsed;

        public int HealAmount => _healAmount;
        public bool SpecialAttackUsed => _specialAttackUsed;

        public Player(string name, int maxHealth, int minAttack, int maxAttack, int healAmount, double specialAttackMultiplier) 
            : base(name, maxHealth, minAttack, maxAttack)
        {
            _healAmount = healAmount;
            _specialAttackMultiplier = specialAttackMultiplier;
            _specialAttackUsed = false;
        }

        public int UseSpecialAttack()
        {
            if (_specialAttackUsed) return 0;
            
            _specialAttackUsed = true;
            int baseDamage = GetAttackDamage();
            return (int)(baseDamage * _specialAttackMultiplier);
        }
        
        public void HealSelf()
        {
             Heal(_healAmount);
        }
    }
}
