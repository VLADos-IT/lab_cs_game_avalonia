using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using cs_prog_17;

namespace lab_cs_game_avalonia;

public partial class MainWindow : Window
{
    private Player? _player;
    private Monster? _monster;
    private bool _isGameRunning;

    public MainWindow()
    {
        InitializeComponent();

        if (AttackButton != null) AttackButton.Click += AttackButton_Click;
        if (HealButton != null) HealButton.Click += HealButton_Click;
        if (SuperAttackButton != null) SuperAttackButton.Click += SuperAttackButton_Click;
        if (NewGameButton != null) NewGameButton.Click += NewGameButton_Click;

        UpdateUI();
        Log("Нажмите 'Новая игра' для начала.");
    }

    private void StartGame()
    {
        _player = new Player("Игрок", 100, 6, 12, 10, 2.0);
        _monster = new Monster("Монстр", 80, 5, 10);
        _isGameRunning = true;

        if (BattleLogPanel != null) BattleLogPanel.Children.Clear();
        Log("Новая битва началась!");
        
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_player == null || _monster == null)
        {
            if (PlayerHpText != null) PlayerHpText.Text = "HP - / -";
            if (PlayerHpBar != null) { PlayerHpBar.Value = 0; }

            if (MonsterHpText != null) MonsterHpText.Text = "HP - / -";
            if (MonsterHpBar != null) { MonsterHpBar.Value = 0; }

            if (AttackButton != null) AttackButton.IsEnabled = false;
            if (HealButton != null) HealButton.IsEnabled = false;
            if (SuperAttackButton != null) SuperAttackButton.IsEnabled = false;
            if (NewGameButton != null) NewGameButton.IsEnabled = true;
            return;
        }

        if (PlayerHpText != null) PlayerHpText.Text = $"HP {_player.CurrentHealth} / {_player.MaxHealth}";
        if (PlayerHpBar != null) 
        {
            PlayerHpBar.Value = _player.CurrentHealth;
            PlayerHpBar.Maximum = _player.MaxHealth;
        }

        if (MonsterHpText != null) MonsterHpText.Text = $"HP {_monster.CurrentHealth} / {_monster.MaxHealth}";
        if (MonsterHpBar != null)
        {
            MonsterHpBar.Value = _monster.CurrentHealth;
            MonsterHpBar.Maximum = _monster.MaxHealth;
        }

        bool playerTurn = _isGameRunning && _player.IsAlive && _monster.IsAlive;

        if (AttackButton != null) AttackButton.IsEnabled = playerTurn;
        if (HealButton != null) HealButton.IsEnabled = playerTurn;
        if (SuperAttackButton != null) SuperAttackButton.IsEnabled = playerTurn && !_player.SpecialAttackUsed;
        if (NewGameButton != null) NewGameButton.IsEnabled = true;
    }

    private void Log(string message)
    {
        if (BattleLogPanel == null) return;
        var textBlock = new TextBlock { Text = message, TextWrapping = TextWrapping.Wrap, Margin = new Avalonia.Thickness(0, 2, 0, 2) };
        BattleLogPanel.Children.Add(textBlock);
    }

    private void AttackButton_Click(object? sender, RoutedEventArgs e)
    {
        if (!_isGameRunning) return;

        int damage = _player.GetAttackDamage();
        _monster.TakeDamage(damage);
        Log($"Игрок атаковал монстра и нанес {damage} урона.");

        EndPlayerTurn();
    }

    private void HealButton_Click(object? sender, RoutedEventArgs e)
    {
        if (!_isGameRunning) return;

        _player.HealSelf();
        Log($"Игрок вылечился на {_player.HealAmount} HP.");

        EndPlayerTurn();
    }

    private void SuperAttackButton_Click(object? sender, RoutedEventArgs e)
    {
        if (!_isGameRunning) return;

        int damage = _player.UseSpecialAttack();
        if (damage > 0)
        {
            _monster.TakeDamage(damage);
            Log($"Игрок использовал СУПЕР-АТАКУ и нанес {damage} урона!");
            EndPlayerTurn();
        }
        else
        {
            Log("Супер-атака уже использована!");
        }
    }

    private void EndPlayerTurn()
    {
        UpdateUI();

        if (!_monster.IsAlive)
        {
            GameOver(true);
            return;
        }

        MonsterTurn();
    }

    private void MonsterTurn()
    {
        if (!_isGameRunning || !_monster.IsAlive) return;

        int damage = _monster.GetAttackDamage();
        _player.TakeDamage(damage);
        Log($"Монстр атаковал игрока и нанес {damage} урона.");

        UpdateUI();

        if (!_player.IsAlive)
        {
            GameOver(false);
        }
    }

    private void GameOver(bool playerWon)
    {
        _isGameRunning = false;
        if (playerWon)
        {
            Log("Поздравляем! Вы победили монстра!");
        }
        else
        {
            Log("Вы проиграли...");
        }
        UpdateUI();
    }

    private void NewGameButton_Click(object? sender, RoutedEventArgs e)
    {
        StartGame();
    }
}