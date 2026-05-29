# SOLID

## S — Single Responsibility
Cada controller cuida só de uma coisa. O `ProdutosController` mexe só com produtos, o `PedidosController` só com pedidos e o `AuthController` só com login e registro.Nenhum deles faz coisa de outro

## D — Dependency Inversion
Os controllers não criam a conexão com o banco diretamente. Eles recebem o `IMongoDatabase` pronto pelo construtor. Quem configura isso é o `Program.cs`.Se um dia mudar o banco, não precisa mexer nos controllers

## O — Open/Closed
O `ProdutosController` foi crescendo sem precisar alterar o que já existia. Os endpoints de listar por categoria, listar disponíveis e atualizar disponibilidade foram adicionados como métodos novos, sem tocar nos métodos de criar, editar e deletar que já funcionavam