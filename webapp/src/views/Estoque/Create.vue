<template>
  <Header></Header>

  <div class="container" style="margin-top: 100px; padding: 0">
    <h2 class="text-center">Adicionar Produto</h2>
    <hr />
    <div class="row mt-5">
      <div class="col"></div>
      <div class="col-md-7">
        <form @submit.prevent="Create">
          <div class="row">
              <div class="col-md-3">
                <label class="control-label">Codigo de Barras</label>
                <input v-model="barCode" class="form-control" />
                <section v-if="formFieldError">
                  <span class="formError">{{ errorBarCode }}</span>
                </section>
              </div>

            <div class="col-md">
                <label class="control-label">Nome</label>
                <input v-model="name" class="form-control" />
                <section v-if="formFieldError">
                  <span class="formError">{{ errorName }}</span>
                </section>
            </div>
          </div>

          <div class="row mt-4">
            <div class="col-lg-3">
              <label class="control-label">Quantidade</label>
              <input v-model="amount" type="number" class="form-control" />
            </div>

            <div class="col-lg-3">
              <label asp-for="ValorUnitario" class="control-label">Valor Unitário</label>
              <input type="number" v-model="unitValue" class="form-control"/>
            </div>

            <div class="col-lg">
              <label class="control-label">Data de Validade</label>
              <input type="date" v-model="expirationDate" class="form-control" />
            </div>
          </div>
          <div class="row" v-if="success">
            <div class="col-md">
              <span
                class="alert alert-success text-center p-3 float-end mt-3"
                role="alert"
                style="width: 100%"
                >{{ alertMessage }}</span
              >
            </div>
          </div>
          <div class="row" v-if="error">
            <div class="col-md">
              <span
                class="alert alert-danger text-center p-3 float-end mt-3"
                role="alert"
                style="width: 100%"
                >{{ danger }}</span
              >
            </div>
          </div>
          <div class="row">
            <div class="col-md">
              <input
                type="submit"
                value="Cadastrar"
                class="btn btn-primary p-3 mt-4 float-end"
              />
            </div>
          </div>
        </form>
      </div>
      <div class="col"></div>
    </div>

    <div>
      <p class="text-center mt-5">
        <router-link class="font-weight-bold" to="/Estoque/Listar"
          >Ir para lista de produtos</router-link
        >
      </p>
    </div>
  </div>

  <Footer></Footer>
</template>

<script>
import Header from "../../components/Header.vue"
import Footer from "../../components/Footer.vue"
import axios from "axios"
import mixins from "../../mixins.js"

export default {
  name: "Home",
  components: {
    Header,
    Footer,
  },
  mixins: [mixins],
  data() {
    return {
      expirationDate:""
    }
  },
  methods: {
    //Adiciona um produto
    async Create() {
      let url = "/Inventory/Create"
      await axios
        .post(
          url,
          {
            barCode: parseInt(this.barCode),
            name: this.name,
            amount: Number(this.amount),
            unitValue: parseFloat(this.unitValue),
            expirationDate: this.expirationDate,
          },
          {
            headers: { "Content-Type": "application/json" },
          }
        )
        .then((response) => {
          this.success = true
          this.alertMessage = response.data
          setTimeout(() => (this.success = false), 3000)
        })
        .catch(error => {
          if(error.response.data.title == "errorBarCode"){
            this.formFieldError = true
            this.errorBarCode = error.response.data.detail
            setTimeout(() => (this.formFieldError = false), 3000)
          }
          else if(error.response.data.title == "errorName"){
            this.formFieldError = true
            this.errorName = error.response.data.detail
            setTimeout(() => (this.formFieldError = false), 3000)
          }
        })
        .finally(() => (this.loading = false))
    },
  },
};
</script>
