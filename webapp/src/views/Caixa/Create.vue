<template>
<Header></Header>
  <div class="container-fluid" id="home" style="margin-top: 100px;">

    <h1 class="text-center">Frente de Caixa</h1>
    <hr>
    <div class="row mt-4">
      <div class="col-md-6">
        <section v-if="error">
          <p class="alert alert-danger">{{ danger }}</p>
        </section>
        <form id="formCaixa">
          <div class="text-danger"></div>
          <div class="form-group">
            <div class="row">
              <div class="col-md-6">
                <label name="lbCodigoBarras" class="control-label">CodigoBarras</label>
                <input ref="barCode" v-on:input="searchItemOnInventoryByBarCode" v-model="barCode" @change="validateForm" id="codigoBarras" name="CodigoBarras" class="form-control">
                <section v-if="formFieldError">
                  <span class="formError">{{ errorBarCode }}</span>
                </section>
              </div>
              <div class="col-md-6">
                <label name="lbNome" class="control-label">Nome</label>
                <input v-model="name" @change="validateForm" id="nome" asp-for="Nome" class="form-control">
                <section v-if="formFieldError">
                  <span class="formError">{{ errorName }}</span>
                </section>
              </div>
              </div>
              <div class="row mt-2">
                <div class="col-2">
                  <label name="lbQuantidade" class="control-label">Quantidade</label>
                  <input v-model="amount" @change="validateForm" id="quantidade" name="Quantidade" class="form-control">
                  <section v-if="formFieldError">
                    <span class="formError">{{ errorAmount }}</span>
                  </section>
                </div>
                <div class="col-3">
                  <label name="lbValorUnitario" class="control-label">Valor Unitário</label>
                  <input v-model="unitValue" @change="validateForm" type="number" id="valorUnitario" name="ValorUnitario" class="form-control" data-thousands="." data-decimal=",">
                  <section v-if="formFieldError">
                    <span class="formError">{{ errorUnitValue }}</span>
                  </section>
                </div>
              </div>
          </div>
          <div class="form-group mt-4">
            <section v-if="listEmpty" >
              <p class="alert alert-danger text-center">{{ listErrorMessage }}</p>
            </section>
            <section class="float-end">
              <a class="btn btn-success p-3 text-white" v-on:click.prevent="addItems">Adicionar Item</a>
              <a class="btn btn-secondary p-3 mx-2 text-white" v-on:click.prevent="updateItems">Atualizar Item</a>
              <a class="btn btn-danger p-3 text-white" v-on:click.prevent="clearItems">Limpar Itens</a>
            </section>
          </div>
        </form>
      </div>

      <div class="col-md-6 mt-4">
        <table class="table table-bordered table-hover">
          <thead class="bg-light">
            <tr>
              <th scope="col">#</th>
              <th scope="col">Código de Barras</th>
              <th scope="col">Nome</th>
              <th scope="col">Qtd:</th>
              <th scope="col">Valor Unitário</th>
              <th scope="col" colspan="2" class="text-center">Ação</th>
            </tr>
          </thead>
          <tbody>
              <tr  v-for="(value,index) in list" v-bind:key="index">
                  <th scope="row">{{ index + 1 }}</th>
                  <td>{{ value.barCode }}</td>
                  <td>{{ value.name }}</td>
                  <td>{{ value.amount }}</td>
                  <td>R$ {{ value.unitValue }}</td>
                  <td>
                      <a v-on:click.prevent="editItem(index)"><i class="fa fa-edit"></i></a>
                  </td>
                  <td> 
                      <a v-on:click.prevent="deleteItem(index)"><i class="fa fa-trash"></i></a>
                  </td>
              </tr>
          </tbody>
        </table>
      </div>

      <div class="col-12" v-if="success">
        <span class="alert alert-success text-center p-3 float-end  mt-3" role="alert" style="width: 50%">{{ alertMessage }}</span>
      </div>

      <div class="col-12">
        <button class="btn btn-primary p-3 float-end mt-3" v-on:click.prevent="Create()">Concluir Venda</button>
      </div>
      
      <div>
        <p class="text-center mt-2">
          <router-link class="mx-5" to="/Caixa/Listar">Ir para lista de vendas</router-link>
        </p>
      </div>
    </div>
  </div>
  <Footer></Footer>
</template>

<script>
import Header from '../../components/Header.vue'
import Footer from '../../components/Footer.vue'
import axios from 'axios'
import mixins from '../../mixins.js'
export default {
  name: 'Home',
  components: {
    Header,
    Footer
  },
  mixins: [mixins],
  methods: {
    //Busca item do estoque pelo codigo de barras
    async searchItemOnInventoryByBarCode() {
        let barCode = this.barCode

        let url = "/Cashier/GetInventoryData/?barCode=" + barCode

        await axios
        .get(url)
        .then((response) => {
          this.name = response.data.name
          this.unitValue = response.data.unitValue
          this.amount = 1
        })
        .catch(error => {
            this.error = true
            this.danger = error.response.data
            setTimeout(() => this.error = false, 3000)
        })
        .finally(() => this.loading = false)
    },
    //Adiciona items somente na tabela para visualizacao
    addItems: function () {
      let listItems = {
          barCode: parseInt(this.barCode),
          name: this.name,
          amount: parseInt(this.amount),
          unitValue: this.$options.filters.currencydecimal(this.unitValue)
      }

      this.list.push(listItems)
    }, 
    //Atualiza os items somente da tabela
    updateItems: function () {
      if (this.list.length > 0) {
          this.list[this.Id].barCode = parseFloat(this.barCode)
          this.list[this.Id].name = this.name
          this.list[this.Id].amount = parseFloat(this.amount)
          this.list[this.Id].unitValue = this.$options.filters.currencydecimal(this.unitValue)
      }
      else{
        this.listEmpty = true
        this.listErrorMessage = "Nenhum item adicionado!"
        setTimeout(() => this.listEmpty = false, 3000)
      }
    }, 
    //Limpa todos os itens dos inputs e tabela
    clearItems: function () {
      this.barCode = ""
      this.name = ""
      this.amount = ""
      this.unitValue = ""
      this.list.splice(0, this.list.length)
    },

    //Adiciona um item da tabela nos inputs para ser editado
    editItem: function (index) {
      if (this.validateForm) {
        this.Id = index
        this.barCode = parseFloat(this.list[index].barCode)
        this.name = this.list[index].name
        this.amount = parseFloat(this.list[index].amount)
        this.unitValue = this.$options.filters.currencydecimal(this.list[index].unitValue, true)
        this.$refs.barCode.focus()
      }
    },
    //Deleta um item da tabela  
    deleteItem: function (index) {
      this.list.splice(index, 1)
      if (this.list.length == 0) {
          this.clearItems()
      }
    }, 
    //Envia todos os itens da tabela para api e adiciona no banco de dados
    async Create() {
      if (this.list.length > 0) {
          let url = "/Cashier/Create"
          let data = []
          this.list.forEach(value => {
              let items = {
                  barCode: value.barCode,
                  name: value.name,
                  amount: value.amount,
                  unitValue: this.$options.filters.currencydecimal(value.unitValue, true)
              }
              data.push(items)
          })
          console.log(data)
          await axios
          .post(url, JSON.stringify(data), { headers: { "Content-Type": "application/json" }})
          .then((response) => {
            this.success = true
            this.alertMessage = response.data
            setTimeout(() => this.success = false, 3000)
          })
          .catch((error) => {
            this.error = true
            this.danger = error
          })
          .finally(() => this.loading = false)
         
          this.list.splice(0, this.list.length)
      }
    }, 
  },
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>