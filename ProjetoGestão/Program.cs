using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGestão
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
        enum Menu { Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4}
        static void Main(string[] args)
        {
            Carregar();

            bool escolheuSair = false;

            while (!escolheuSair)
            {

                Console.WriteLine("Sistema de Clientes - Bem vindo!");
                Console.WriteLine("1 - Listagem\n2 - Adicionar\n3 - Remover\n4 - Sair");
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
                        escolheuSair = true;
                        break;


                }

                Console.Clear();

            }
           
        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de Clientes: ");
            Console.Write("Nome do Cliente: ");
            cliente.nome = Console.ReadLine();
            Console.Write("Email do Cliente: ");
            cliente.email = Console.ReadLine();
            Console.Write("CPF do Cliente: ");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro concluído! Aperte Enter para sair.");
            Console.ReadLine();
        }

        static void Listagem()
        {

            if(clientes.Count > 0) //se tem pelo menos um cliente cadastrado, insira a lógica abaixo
            {
                Console.WriteLine("Lista de Clientes: ");

                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: { cliente.nome }");
                    Console.WriteLine($"Email: { cliente.email }");
                    Console.WriteLine($"CPF: { cliente.cpf }");
                    i++;
                    Console.WriteLine("============================");
                }

            }
            else
            {
                Console.WriteLine("Não há nenhum cliente cadastrado!");
            }

            Console.WriteLine("Aperte enter para sair.");
            Console.ReadLine();

        }

        static void Remover()
        {
            Listagem();
            Console.Write("Digite o ID do cliente que você deseja remover: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
                
            }
            else
            {
                Console.WriteLine("Id inválido. Tente novamente.");
                Console.ReadLine();
            }

        }

        static void Salvar()
        {
            FileStream stream = new FileStream("cliente.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);

            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("cliente.dat", FileMode.OpenOrCreate);

            try //ela vai tentar executar o bloco de código. Se tiver um erro, ele não vai parar o programa.
            {
               
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream); //vai ler uma lista de clientes do arquivo

                if(clientes == null)
                {
                    clientes = new List<Cliente>(); //não vai deixar a variável como nula, vai criar uma lista de clientes
                }

                
            }
            catch(Exception e) //aqui conseguirei tratar o erro encontrado acima
            {
                clientes = new List<Cliente>();
            }

            stream.Close();

        }
    }
}
