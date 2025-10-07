@echo off
echo ========================================
echo BrainFlow - Configuracao do Banco
echo ========================================
echo.
echo IMPORTANTE: Certifique-se de que o MySQL esteja rodando no XAMPP!
echo.
pause

echo Criando banco e tabelas...
"C:\xampp\mysql\bin\mysql.exe" -u root -e "source 'c:/Users/Mais que Café/Documents/Ederson Luan/TCC/TCCBrain/BrainFlow.Repository/Database/Scripts/001_InitialSchema.sql'"

if %ERRORLEVEL% EQU 0 (
    echo ✓ Schema criado com sucesso!
    echo.
    echo Inserindo dados de teste...
    "C:\xampp\mysql\bin\mysql.exe" -u root -e "source 'c:/Users/Mais que Café/Documents/Ederson Luan/TCC/TCCBrain/BrainFlow.Repository/Database/SeedData/SeedData_Basic.sql'"
    
    if %ERRORLEVEL% EQU 0 (
        echo ✓ Dados de teste inseridos com sucesso!
        echo.
        echo ========================================
        echo CONFIGURACAO COMPLETA!
        echo ========================================
        echo.
        echo Credenciais para teste:
        echo - Admin: admin@brainflow.com / password
        echo - Afiliado: maria@exemplo.com / password
        echo - Estudante: joao@exemplo.com / password
        echo.
        echo phpMyAdmin: http://localhost/phpmyadmin
        echo ========================================
    ) else (
        echo ✗ Erro ao inserir dados de teste!
    )
) else (
    echo ✗ Erro ao criar schema!
)

echo.
pause