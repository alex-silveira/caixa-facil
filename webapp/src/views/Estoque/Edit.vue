<template>
  <Header></Header>
  <div class="container" style="margin-top: 100px; padding: 0">
    <h2 class="text-center">Atualizar Produto</h2>
    <hr />
    <div class="row mt-5">
      <div class="col"></div>
      <div class="col-md-7">
        <form @submit.prevent="Update">
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
                value="Atualizar"
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
  data() {
    return {
      defaultUrl:"/Inventory/",
      id: null,
      expirationDate: "",
      inventoryId: null
    }
  },
  mounted() {
    //Busca o id da rota e chama o metodo para preencher os inputs 
    this.id = this.$route.query.id
    this.getProductById()
  },
  methods: {
    //Busca o produto pelo id e preenche os inputs
    async getProductById() {
    await axios
    .get(this.defaultUrl + "Edit/?id=" + this.id)
      .then((response) => {
        this.inventoryId = response.data.inventoryId
        this.barCode = response.data.barCode
        this.name = response.data.name
        this.amount = response.data.amount
        this.unitValue = response.data.unitValue 
        this.expirationDate = this.$options.filters.fieldTypeDateFormat(response.data.expirationDate)       
      })
      .catch(() => {
          this.errored = true
      })
      .finally(() => this.loading = false)
    },
    //Atualiza o item pelo inventoryId
    async Update(){
      let url = this.defaultUrl + "Edit/?inventoryId=" + this.inventoryId
      console.log(url)
      await axios
        .post(
          url,
          {
            inventoryId: this.inventoryId,
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
          setTimeout(() => (this.success = false, this.$router.push({path: '/Estoque/Listar'})), 3000)
        })
        .catch((error) => {
            console.log(error)
            this.errored = true
        })
        .finally(() => this.loading = false)
    }
  },
}
</script>