<?php

require 'conn.php';

if ($_SERVER['REQUEST_METHOD'] == 'GET') {
    $sql = 'SELECT * FROM transactionhistory';
    $stmt = $pdo->query($sql);
    $payments = $stmt->fetchAll();
    echo json_encode($payments);
}
