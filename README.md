# Correios RESTful API

API para desenvolvedores utilizarem em aplicações que precisem encontrar um endereço a partir do CEP informado. (Os dados vem direto dos correios)

### Acesse: [http://correiosapi.apphb.com/cep/76873274](http://correiosapi.apphb.com/cep/76873274)

e veja um exemplo de resultado!

### Retornos:

Em caso de sucesso:

```json
{
    Endereco: {
        Bairro: "Setor Institucional"
        Cep: "76872862"
        Cidade: "Ariquemes"
        Estado: "RO"
        Logradouro: "Rio Madeira "
        TipoDeLogradouro: "Rua"
    }
    Mensagem: "CEP encontrado com sucesso"
    TemErro: false
}
```

Em caso de erro:

```json
{
    Endereco: null
    Mensagem: "CEP não encontrado"
    TemErro: true
}
```

### Exemplos de uso

Jquery:

```javascript
$.ajax({
  url: 'http://correiosapi.apphb.com/cep/78930000',
  dataType: 'jsonp',
  crossDomain: true,
  contentType: "application/json",
  success: function(response){
    console.log(response);
  }
});
```

### Contribua

Faça um fork do projeto e ajude a melhorar o código, isso foi só uma brincadeira na madrugada!