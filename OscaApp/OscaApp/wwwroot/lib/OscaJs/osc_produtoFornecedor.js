//********************************* Biblioteca com funções da Tela Produto do Pedido ***************

function DeleteProdutoFornecedor(id, idProduto) {

    DeleteService(id, "ProdutoFornecedorAPI", "GridProdutoFornecedor?idProduto=" + idProduto);
} 




