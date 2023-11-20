using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        FrotaTransporte frota = new FrotaTransporte();

        int opcao;
        do
        {
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("0. Finalizar");
            Console.WriteLine("1. Cadastrar veículo");
            Console.WriteLine("2. Cadastrar garagem");
            Console.WriteLine("3. Iniciar jornada");
            Console.WriteLine("4. Encerrar jornada");
            Console.WriteLine("5. Liberar viagem de uma determinada origem para um determinado destino");
            Console.WriteLine("6. Listar veículos em determinada garagem (informando a quantidade de veículos e seu potencial de transporte)");
            Console.WriteLine("7. Informar quantidade de viagens efetuadas de uma determinada origem para um determinado destino");
            Console.WriteLine("8. Listar viagens efetuadas de uma determinada origem para um determinado destino");
            Console.WriteLine("9. Informar quantidade de passageiros transportados de uma determinada origem para um determinado destino");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    frota.CadastrarVeiculo();
                    break;
                case 2:
                    frota.CadastrarGaragem();
                    break;
                case 3:
                    frota.IniciarJornada();
                    break;
                case 4:
                    frota.EncerrarJornada();
                    break;
                case 5:
                    frota.LiberarViagem();
                    break;
                case 6:
                    frota.ListarVeiculosEmGaragem();
                    break;
                case 7:
                    frota.InformarQuantidadeViagens();
                    break;
                case 8:
                    frota.ListarViagens();
                    break;
                case 9:
                    frota.InformarQuantidadePassageiros();
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

        } while (opcao != 0);
    }
}

class Veiculo
{
    public int Numero { get; set; }
    public int Capacidade { get; set; }
    public int PassageirosTransportados { get; set; }
}

class Garagem
{
    public string Origem { get; set; }
    public string Destino { get; set; }
    public List<Veiculo> VeiculosEstacionados { get; set; } = new List<Veiculo>();
}

class FrotaTransporte
{
    private List<Veiculo> veiculos = new List<Veiculo>();
    private List<Garagem> garagens = new List<Garagem>();
    private bool jornadaIniciada = false;

    public void CadastrarVeiculo()
    {
        if (!jornadaIniciada)
        {
            Veiculo veiculo = new Veiculo();
            Console.WriteLine("Digite o número do veículo:");
            veiculo.Numero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a capacidade do veículo:");
            veiculo.Capacidade = int.Parse(Console.ReadLine());

            veiculos.Add(veiculo);
            Console.WriteLine("Veículo cadastrado com sucesso.");
        }
        else
        {
            Console.WriteLine("A jornada já foi iniciada. Cadastros de veículos não são permitidos.");
        }
    }

    public void CadastrarGaragem()
    {
        if (!jornadaIniciada)
        {
            Garagem garagem = new Garagem();
            Console.WriteLine("Digite a origem da garagem:");
            garagem.Origem = Console.ReadLine();

            Console.WriteLine("Digite o destino da garagem:");
            garagem.Destino = Console.ReadLine();

            garagens.Add(garagem);
            Console.WriteLine("Garagem cadastrada com sucesso.");
        }
        else
        {
            Console.WriteLine("A jornada já foi iniciada. Cadastros de garagens não são permitidos.");
        }
    }

    public void IniciarJornada()
    {
        if (!jornadaIniciada)
        {
            if (veiculos.Count == 0 || garagens.Count == 0)
            {
                Console.WriteLine("Não há veículos ou garagens cadastrados para iniciar a jornada.");
            }
            else
            {
                DistribuirVeiculos();
                Console.WriteLine("Jornada iniciada. Veículos distribuídos entre as garagens.");
                jornadaIniciada = true;
            }
        }
        else
        {
            Console.WriteLine("A jornada já foi iniciada.");
        }
    }

    public void EncerrarJornada()
    {
        if (jornadaIniciada)
        {
            foreach (Garagem garagem in garagens)
            {
                Console.WriteLine($"Garagem {garagem.Origem} para {garagem.Destino}:");
                foreach (Veiculo veiculo in garagem.VeiculosEstacionados)
                {
                    Console.WriteLine($"Veículo {veiculo.Numero}: {veiculo.PassageirosTransportados} passageiros transportados.");
                }
                garagem.VeiculosEstacionados.Clear();
            }
            jornadaIniciada = false;
            Console.WriteLine("Jornada encerrada. Veículos retornados às garagens.");
        }
        else
        {
            Console.WriteLine("A jornada ainda não foi iniciada.");
        }
    }

    public void LiberarViagem()
    {
        if (jornadaIniciada)
        {
            Console.WriteLine("Digite a origem da viagem:");
            string origem = Console.ReadLine();

            Console.WriteLine("Digite o destino da viagem:");
            string destino = Console.ReadLine();

            Garagem garagemOrigem = garagens.Find(g => g.Origem == origem);
            Garagem garagemDestino = garagens.Find(g => g.Destino == destino);

            if (garagemOrigem != null && garagemDestino != null)
            {
                if (garagemOrigem.VeiculosEstacionados.Count > 0)
                {
                    Veiculo veiculo = garagemOrigem.VeiculosEstacionados.First();
                    Console.WriteLine($"Viagem liberada: Veículo {veiculo.Numero} de {garagemOrigem.Origem} para {garagemDestino.Destino}.");
                    garagemDestino.VeiculosEstacionados.Add(veiculo);
                    garagemOrigem.VeiculosEstacionados.Remove(veiculo);
                }
                else
                {
                    Console.WriteLine($"Nenhum veículo disponível em {garagemOrigem.Origem} para liberar viagem.");
                }
            }
            else
            {
                Console.WriteLine("Origem ou destino da viagem não encontrados.");
            }
        }
        else
        {
            Console.WriteLine("A jornada ainda não foi iniciada.");
        }
    }

    public void ListarVeiculosEmGaragem()
    {
        Console.WriteLine("Digite a origem da garagem:");
        string origem = Console.ReadLine();

        Garagem garagem = garagens.Find(g => g.Origem == origem);

        if (garagem != null)
        {
            Console.WriteLine($"Veículos em {garagem.Origem} para {garagem.Destino}:");
            foreach (Veiculo veiculo in garagem.VeiculosEstacionados)
            {
                Console.WriteLine($"Veículo {veiculo.Numero}: Capacidade {veiculo.Capacidade}");
            }
        }
        else
        {
            Console.WriteLine("Garagem não encontrada.");
        }
    }

    public void InformarQuantidadeViagens()
    {
        Console.WriteLine("Digite a origem da viagem:");
        string origem = Console.ReadLine();

        Console.WriteLine("Digite o destino da viagem:");
        string destino = Console.ReadLine();

        int quantidadeViagens = 0;

        foreach (Garagem garagem in garagens)
        {
            if (garagem.Origem == origem && garagem.Destino == destino)
            {
                quantidadeViagens = garagem.VeiculosEstacionados.Count;
                break;
            }
        }

        Console.WriteLine($"Quantidade de viagens de {origem} para {destino}: {quantidadeViagens}");
    }

    public void ListarViagens()
    {
        Console.WriteLine("Digite a origem da viagem:");
        string origem = Console.ReadLine();

        Console.WriteLine("Digite o destino da viagem:");
        string destino = Console.ReadLine();

        foreach (Garagem garagem in garagens)
        {
            if (garagem.Origem == origem && garagem.Destino == destino)
            {
                Console.WriteLine($"Viagens de {origem} para {destino}:");
                foreach (Veiculo veiculo in garagem.VeiculosEstacionados)
                {
                    Console.WriteLine($"Veículo {veiculo.Numero}: {veiculo.PassageirosTransportados} passageiros transportados.");
                }
                break;
            }
        }
    }

    public void InformarQuantidadePassageiros()
    {
        Console.WriteLine("Digite a origem da viagem:");
        string origem = Console.ReadLine();

        Console.WriteLine("Digite o destino da viagem:");
        string destino = Console.ReadLine();

        int quantidadePassageiros = 0;

        foreach (Garagem garagem in garagens)
        {
            if (garagem.Origem == origem && garagem.Destino == destino)
            {
                foreach (Veiculo veiculo in garagem.VeiculosEstacionados)
                {
                    quantidadePassageiros += veiculo.PassageirosTransportados;
                }
                break;
            }
        }

        Console.WriteLine($"Quantidade de passageiros transportados de {origem} para {destino}: {quantidadePassageiros}");
    }

    private void DistribuirVeiculos()
    {
        int i = 0;
        foreach (Garagem garagem in garagens)
        {
            if (i >= veiculos.Count)
            {
                i = 0;
            }
            Veiculo veiculo = veiculos[i];
            garagem.VeiculosEstacionados.Add(veiculo);
            i++;
        }
    }
}
