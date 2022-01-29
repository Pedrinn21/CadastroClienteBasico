using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;


namespace Projeto2
{
    class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();


        enum Menu { Listagem=1, Adicionar=2, Remover=3, Sair=4 }

        static void Main(string[] args)
        {
            Carregar();
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("Sistema de clientes - Bem Vindo!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");

                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        sair = true;
                        break;
                }
                Console.Clear();
            }

            static void Adicionar()
            {
                Cliente cliente = new Cliente();
                Console.WriteLine("Cadastro de Cliente: ");
                Console.WriteLine("Nome do cliente: ");
                cliente.nome = Console.ReadLine();

                Console.WriteLine("Email do cliente: ");
                cliente.email = Console.ReadLine();

                Console.WriteLine("CPF do cliente: ");
                cliente.cpf = Console.ReadLine();

                clientes.Add(cliente);
                Salvar();

                Console.WriteLine("Cadastro concluído, aperte ENTER para sair.");
                Console.ReadLine();
            }

            static void Listagem()
            {

                if(clientes.Count > 0)  //SE TEM PELO MENOS UM CLIENTE CADASTRADO
                {
                    Console.WriteLine("Lista de clientes: ");
                    int i = 0;
                    foreach (Cliente cliente in clientes)
                    {
                        Console.WriteLine($"ID: {i}");
                        Console.WriteLine($"Nome: {cliente.nome}");
                        Console.WriteLine($"E-mail: {cliente.email}");
                        Console.WriteLine($"CPF: {cliente.cpf}");
                        Console.WriteLine("==============================");
                        i++;
                        
                    }
                }
                else
                {
                    Console.WriteLine("Nenhum cliente cadastrado.");
                }

                Console.WriteLine("Aperte ENTER para sair.");
                Console.ReadLine();
            }
        
            static void Salvar()
            {
                FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
                BinaryFormatter encoder = new BinaryFormatter();

                encoder.Serialize(stream, clientes);

                stream.Close();
            }
        
            static void Carregar()
            {
                FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);

                try
                {
                    BinaryFormatter encoder = new BinaryFormatter();

                    clientes = (List<Cliente>)encoder.Deserialize(stream);

                    if(clientes == null)
                    {
                        clientes = new List<Cliente>();
                    }
                }
                catch(Exception ex)
                {
                    clientes = new List<Cliente>();
                }

                stream.Close();
            }
        
            static void Remover()
            {
                Listagem();

                Console.WriteLine("Digite o ID do cliente que você quer remover: ");
                int id = int.Parse(Console.ReadLine());
                
                if(id >= 0 && id < clientes.Count)
                {
                    clientes.RemoveAt(id);
                    Salvar();
                }
                else
                {
                    Console.WriteLine("ID digitado é inválido, tente novamente!");
                    Console.ReadLine();
                }
            }

        }
    }
}