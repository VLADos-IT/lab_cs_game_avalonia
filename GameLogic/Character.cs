using System;

namespace cs_prog_17
{
	public class Character
	{
		protected string _name;
		protected int _currentHealth;
		protected int _maxHealth;
		protected Range<int> _attackRange;
		protected Random _random;

		public string Name => _name;
		public int CurrentHealth => _currentHealth;
		public int MaxHealth => _maxHealth;
		public int MinAttack => _attackRange.Min;
		public int MaxAttack => _attackRange.Max;
		public bool IsAlive => _currentHealth > 0;

		public Character(string name, int maxHealth, int minAttack, int maxAttack)
		{
			_name = name;
			_maxHealth = maxHealth;
			_currentHealth = maxHealth;
			_attackRange = new Range<int>(minAttack, maxAttack);
			_random = new Random();
		}

		public virtual int GetAttackDamage()
		{
			return _random.Next(_attackRange.Min, _attackRange.Max + 1);
		}

		public void TakeDamage(int damage)
		{
			_currentHealth -= damage;
			if (_currentHealth < 0) _currentHealth = 0;
		}

		public void Heal(int amount)
		{
			_currentHealth += amount;
			if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
		}
	}
}
