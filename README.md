# BITSIGN.Integrador
###### Envio e Recebimentos de documentos para assinatura.
Utilitário para envio e recebimentos de pacotes para assinatura. Pode ser utlizado por aplicações que não desejam interagir com as API's da BITSIGN, e que utilizam diretórios para disponibilizar os arquivos para assinatura ou capturar os arquivos assinados. A responsabilidade da aplicação passa a ser somente a geração e o processamento destes arquivos, uma vez que este utilitário conecta e faz o envio dos arquivos para a BITSIGN através das API's.

O pacote consite em um arquivo ZIP, contendo o manifesto (que pode ser um arquivo XML ou JSON) que contém todas as instruções para coletar as assinaturas, bem como a relação de todos os documentos que precisam ser assinados. A seguir está os link que aponta para a documentação e que dá maiores detalhes sobre a estruturação deste pacote:

> Pacote ZIP: [https://bitsign.com.br/documentacao#integracaoPacotes](https://bitsign.com.br/documentacao#integracaoPacotes)

Antes de utilizar a ferramenta, é necessário é informar as configurações através do arquivo `appsettings.json`, que consiste nos parâmetros informados no momento da contratação do serviço e o diretório raiz onde os arquivos serão gerados ou armazenados. Esse arquivo deve ser devidamente configurado antes de fazer uso desta ferramenta.
```json
{
  "BITSIGN.Proxy": {
    "Conexoes": [
      {
        "Nome": "App de Teste",
        "Ambiente": "Sandbox",
        "Versao": "v1",
        "CodigoDoContratante": "c7efea7b-5040-4956-9d11-755dbeaddf5a",
        "ChaveDeIntegracao": "CHAVE-FORNECIDA-PELO-SERVIÇO-DA-BITSIGN",
        "FormatoDeSerializacao": "Json",
        "Timeout": "00:00:30"
      }
    ]
  },
  "Paths": {
    "Dados": "C:\\Temp\\Teste"
  }
}
```
A partir deste diretório raiz, serão criados subdiretórios para organização dos arquivos, onde os arquivos gerados por outras aplicação para envio, deverão ser colocados no diretório `Remessas` e, quando ele foi enviado ao serviço, a ferramenta moverá o arquivo para o subdiretório `Enviadas`. Quando novos arquivos estiverem disponíveis, ao realizar a download, ele será armazenado no subdiretório `Retornos`, que é onde as aplicações deverão monitorar para processamento. Por fim, todas as execuções são registradas e armazenadas em arquivos de logs diários no subdiretório `Logs`.
```
* C:\Temp\Teste
|---- Remessas
|-------- Enviadas
|---- Retornos
|---- Logs
```
> O diretório *Logs* armazenará os logs de execução, gerando um arquivo por dia, no formato **AAMMDD.txt**.

Para executar a ferramenta, você poderá utilizar um dos parâmetros que se pode ver a seguir. Basicamente, se precisa sincronizar, que é onde os arquivos são enviados e aqueles que estiverem disponíveis são baixados, basta omitir os parâmetros. Se desejar executar uma ação específica (`Remessa` ou `Retorno`), basta informar o parâmetro ao executar.
```
BITSIGN.Integrador
Envio e Recebimentos de documentos para assinatura.
Copyright (C) 2023 - BITSIGN(R) - Assinaturas Digitais

Uso:
    Sincronizar (Remessa e Retorno):
        BSI.exe
    Enviar Lotes:
        BSI.exe Remessa
    Recepcionar Lotes:
        BSI.exe Retorno
    Recepcionar Lotes de um Período:
        BSI.exe Retorno 00/00/0000 00/00/0000
    Recepcionar Lote Específico:
        BSI.exe Retorno 00000000-0000-0000-0000-000000000000
```
> A busca por arquivos assinados está, por padrão, fixada nos arquivos gerados no dia/hora (`DateTime.Now`) em que a ferramenta está sendo executada; caso precise de dias ou períodos específicos, parametrize a data desejada ao executar a ferramenta.

### Execução Periódica
Alternativamente poderá agendar este executável para que ele seja executado periodicamente. Caso precise disso, pode recorrer à recursos nativos do próprio sistema operacional (como as Tarefas do Windows), para que se configure o período e a quantidade de repetições necessárias.

> #### CONTATOS
>
> - Site: <https://bitsign.com.br>
> - E-mail Técnico: <dev@bitfin.com.br>
> - E-mail Comercial: <contato@bitsign.com.br>
> - Telefone (+WhatsApp): +55 (19) 9.9901-1065
