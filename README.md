# Address RESTful API

API para desenvolvedores utilizarem em aplicações que precisem encontrar um endereço a partir do CEP informado. (Os dados vem direto da base de dados dos correios)

### Acesse: [http://correiosapi.apphb.com/cep/76873274](http://correiosapi.apphb.com/cep/76873274)

e veja um exemplo de resultado!

### Retornos:

Em caso de sucesso:

```json
{
  "bairro": "Setor Institucional"
  "cep": "76872862"
  "cidade": "Ariquemes"
  "estado": "RO"
  "logradouro": "Rio Madeira "
  "tipoDeLogradouro": "Rua"
}
```

Em caso de erro de CEP não encontrado a API retornará o HTTP Status 404

### Exemplos de uso

Jquery:

```javascript
$.ajax({
  url: 'http://correiosapi.apphb.com/cep/76873274',
  dataType: 'jsonp',
  crossDomain: true,
  contentType: "application/json",
  statusCode: {
    200: function(data) { console.log(data); } // Ok
    ,400: function(msg) { console.log(msg);  } // Bad Request
    ,404: function(msg) { console.log("CEP não encontrado!!"); } // Not Found
  }
});​
```

### Contribua

Faça um fork do projeto e ajude a melhorar o código, isso foi só uma brincadeira na madrugada!
