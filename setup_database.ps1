# ========================================
# BrainFlow - Configuração do Banco (PowerShell)
# ========================================

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "BrainFlow - Configuração do Banco" -ForegroundColor Yellow
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Verificar se XAMPP MySQL está disponível
if (!(Test-Path "C:\xampp\mysql\bin\mysql.exe")) {
    Write-Host "ERRO: MySQL do XAMPP não encontrado!" -ForegroundColor Red
    Write-Host "Certifique-se de que o XAMPP está instalado em C:\xampp\" -ForegroundColor Yellow
    pause
    exit
}

Write-Host "IMPORTANTE: Certifique-se de que o MySQL esteja rodando no XAMPP!" -ForegroundColor Yellow
Write-Host ""
Read-Host "Pressione Enter para continuar"

# Executar schema
Write-Host "Criando banco e tabelas..." -ForegroundColor Green
$schemaPath = "c:\Users\Mais que Café\Documents\Ederson Luan\TCC\TCCBrain\BrainFlow.Repository\Database\Scripts\001_InitialSchema.sql"
$seedPath = "c:\Users\Mais que Café\Documents\Ederson Luan\TCC\TCCBrain\BrainFlow.Repository\Database\SeedData\SeedData_Basic.sql"

try {
    Get-Content $schemaPath | & "C:\xampp\mysql\bin\mysql.exe" -u root
    Write-Host "✓ Schema criado com sucesso!" -ForegroundColor Green
    Write-Host ""
    
    Write-Host "Inserindo dados de teste..." -ForegroundColor Green
    Get-Content $seedPath | & "C:\xampp\mysql\bin\mysql.exe" -u root
    Write-Host "✓ Dados de teste inseridos com sucesso!" -ForegroundColor Green
    Write-Host ""
    
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "CONFIGURAÇÃO COMPLETA!" -ForegroundColor Yellow
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Credenciais para teste:" -ForegroundColor White
    Write-Host "- Admin: admin@brainflow.com / password" -ForegroundColor Gray
    Write-Host "- Afiliado: maria@exemplo.com / password" -ForegroundColor Gray
    Write-Host "- Estudante: joao@exemplo.com / password" -ForegroundColor Gray
    Write-Host ""
    Write-Host "phpMyAdmin: http://localhost/phpmyadmin" -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan
    
} catch {
    Write-Host "✗ Erro durante a configuração: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host ""
Read-Host "Pressione Enter para sair"