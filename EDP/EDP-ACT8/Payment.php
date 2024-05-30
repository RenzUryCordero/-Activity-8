<?php

require 'conn.php';

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $input = json_decode(file_get_contents('php://input'), true);
    $sql = 'INSERT INTO payments (name, amount, number) VALUES (?, ?, ?)';

    $stmt = $pdo->prepare($sql);
    $stmt->execute([$input['name'], $input['amount'], $input['number']]);
    echo json_encode('Payment added');

    $sql = 'SELECT PaymentID FROM payments WHERE name = ? AND amount = ? AND number = ?';
    $stmt = $pdo->prepare($sql);
    $stmt->execute([$input['name'], $input['amount'], $input['number']]);
    $payment = $stmt->fetch();

    $sql = 'INSERT INTO transactionhistory (PaymentID, name, amount, number) VALUES (?, ?, ?, ?)';
    $stmt = $pdo->prepare($sql);
    $stmt->execute([$payment['PaymentID'], $input['name'], $input['amount'], $input['number']]);
    echo json_encode('Transaction added');
}

if ($_SERVER['REQUEST_METHOD'] == 'GET') {
    $sql = 'SELECT * FROM payments';
    $stmt = $pdo->query($sql);
    $payments = $stmt->fetchAll();
    echo json_encode($payments);
}
