using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Projeto2
{
    class Program
    {
        [System.Serializable]
        struct Usuario
        {
            public int Id;
            public string Name;
            public string Login;
            public string Password;
        }

        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Usuario> usuarios = new List<Usuario>();

        static List<Cliente> clientes = new List<Cliente>();


        enum Menu { Listagem=1, Adicionar=2, Remover=3, Controle_Usuario = 4, Sair = 5 }

        enum Menu_Usuario { Listagem=1, Adicionar = 2, Remover=3, Sair = 4}

        static void Main(string[] args)
        {
            bool aut = false;

            aut = Login();      //AUT CHAMA O METODO LOGIN E SALVA O RETORNO DELE

            if(aut == true)     //SE O METODO LOGIN RETORNAR TRUE SIGNIFICA QUE USUARIO CONSEGUIU SE LOGAR,
            {                   // A PARTIR DISSO MENU É LIBERADO
                MenuOp();
            }

        }

        static void MenuOp()
        {
            Carregar_Cliente();
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("Sistema de clientes - Bem Vindo!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Controle de Usuario\n5-Sair");

                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Listagem:
                        Listagem_Cliente();
                        break;
                    case Menu.Adicionar:
                        Adicionar_Cliente();
                        break;
                    case Menu.Remover:
                        Remover_Cliente();
                        break;
                    case Menu.Controle_Usuario:
                        Controle_Usuario();
                        break;
                    case Menu.Sair:
                        sair = true;
                        break;
                    
                }
                Console.Clear();
            }
        }

        static void Adicionar_Cliente()
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
            Salvar_Cliente();

            Console.WriteLine("Cadastro concluído, aperte ENTER para sair.");
            Console.ReadLine();
        }

        static void Listagem_Cliente()
        {

            if (clientes.Count > 0)  //SE TEM PELO MENOS UM CLIENTE CADASTRADO
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

        static void Salvar_Cliente()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);

            stream.Close();
        }

        static void Carregar_Cliente()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);

            try
            {
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);

                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }
            }
            catch (Exception ex)
            {
                clientes = new List<Cliente>();
            }

            stream.Close();
        }

        static void Remover_Cliente()
        {
            Listagem_Cliente();

            Console.WriteLine("Digite o ID do cliente que você quer remover: ");
            int id = int.Parse(Console.ReadLine());

            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar_Cliente();
            }
            else
            {
                Console.WriteLine("ID digitado é inválido, tente novamente!");
                Console.ReadLine();
            }
        }

        static bool Login()     //SE USUARIO CONSEGUIR LOGAR METODO RETORNA TRUE
        {
            string usuario_login;
            string senha_login;

            bool sair = false;

            while (!sair)
            {

                int contrllogin = 0;        //VARIAVEL QUE CONTROLA SE IRA TENTAR LOGAR DNV OU ENCERRAR PROGRAM

                Console.WriteLine("CONTROLE DE CLIENTE - LOGIN:\n");
                Console.WriteLine("Usuario: ");
                usuario_login = Console.ReadLine();
                Console.WriteLine("Senha: ");
                senha_login = Console.ReadLine();

                Carrega_Usuario();

                foreach (Usuario usuario in usuarios)
                {
                    if (usuario_login == usuario.Login)
                    {
                        if (senha_login == usuario.Password)
                        {
                            Console.WriteLine("LOGADO COM SUCESSO!");
                            Console.ReadLine();
                            Console.Clear();
                            //MenuOp();
                            
                            sair = true;
                            contrllogin = 1;
                        }
                    }
                }
                if(contrllogin == 0)
                {
                    Console.WriteLine("Usuario incorreto ou inexistente!");

                    Console.ReadLine();
                    Console.Clear();
                }
            }
            return true;
        }

        static void Controle_Usuario()
        {
            Console.Clear();
            Carrega_Usuario();
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("Sistema de clientes - CONTROLE DE USUARIOS!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");

                int intOp = int.Parse(Console.ReadLine());
                Menu_Usuario opcao = (Menu_Usuario)intOp;

                switch (opcao)
                {
                    case Menu_Usuario.Listagem:
                        Listar_Usuario();
                        break;
                    case Menu_Usuario.Adicionar:
                        Adicionar_Usuario();
                        break;
                    case Menu_Usuario.Remover:
                        
                        break;
                    case Menu_Usuario.Sair:
                        sair = true;
                        break;
                }
                Console.Clear();
            }
        }

        static void Carrega_Usuario()
        {
            FileStream streamusuario = new FileStream("usuarios.dat", FileMode.OpenOrCreate);

            try
            {
                BinaryFormatter encoder = new BinaryFormatter();

                usuarios = (List<Usuario>)encoder.Deserialize(streamusuario);

                if (usuarios == null)
                {
                    usuarios = new List<Usuario>();
                }
            }
            catch (Exception ex)
            {
                usuarios = new List<Usuario>();
            }

            streamusuario.Close();
        }
        
        static void Adicionar_Usuario()
        {
            Usuario usuario = new Usuario();
            Console.WriteLine("Cadastro de Usuario: ");
            Console.WriteLine("Nome do usuario: ");
            usuario.Name = Console.ReadLine();

            Console.WriteLine("Login do usuario: ");
            usuario.Login = Console.ReadLine();

            Console.WriteLine("Senha do usuario: ");
            usuario.Password = Console.ReadLine();

            usuarios.Add(usuario);
            Salvar_Login();

            Console.WriteLine("Cadastro concluído, aperte ENTER para sair.");
            Console.ReadLine();
        }

        static void Salvar_Login()
        {
            FileStream streamusuario = new FileStream("usuarios.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(streamusuario, usuarios);

            streamusuario.Close();
        }

        static void Listar_Usuario()
        {

            if (usuarios.Count > 0)  //SE TEM PELO MENOS UM USUARIO CADASTRADO
            {
                Console.WriteLine("Lista de usuarios: ");
                int i = 0;
                foreach (Usuario usuario in usuarios)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {usuario.Name}");
                    Console.WriteLine($"Login: {usuario.Login}");
                    Console.WriteLine("==============================");
                    i++;

                }
            }
            else
            {
                Console.WriteLine("Nenhum usuario cadastrado.");
            }

            Console.WriteLine("Aperte ENTER para sair.");
            Console.ReadLine();
        }

        static void Remove_Usuario()
        {
            Listar_Usuario();

            Console.WriteLine("Digite o ID do usuario que você quer remover: ");
            int id = int.Parse(Console.ReadLine());

            if (id >= 0 && id < usuarios.Count)
            {
                usuarios.RemoveAt(id);
                Salvar_Login();
            }
            else
            {
                Console.WriteLine("ID digitado é inválido, tente novamente!");
                Console.ReadLine();
            }
        }


    }
}