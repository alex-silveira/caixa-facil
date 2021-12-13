<template>
<Header></Header>

<section class="container-fluid" style="margin-top: 100px;">
    <table class="table table-bordered table-hover" id="tableCaixa">
        <thead>
            <tr class="bg-light">
                <th colspan="12" class="text-center"><h2 class="text-center">Vendas Realizadas</h2></th>
            </tr>
        </thead>
        <thead class="text-center">
            <tr>
                <th scope="col">
                    <a v-on:click="arrowClick"><i id="arrow-up-barCode" class="fa fa-arrow-up"></i></a> Cod. Barras <a v-on:click="arrowClick"><i id="arrow-down-barCode" class="fa fa-arrow-down"></i></a>
                </th>
                <th scope="col">
                    <a v-on:click="arrowClick"><i id="arrow-up-name" class="fa fa-arrow-up"></i></a> Nome <a v-on:click="arrowClick"><i id="arrow-down-name" class="fa fa-arrow-down"></i></a>
                </th>
                <th scope="col">
                    <a v-on:click="arrowClick"><i id="arrow-up-amount" class="fa fa-arrow-up"></i></a> Qtd <a v-on:click="arrowClick"><i id="arrow-down-amount" class="fa fa-arrow-down"></i></a>
                </th>
                <th scope="col">
                    <a v-on:click="arrowClick"><i id="arrow-up-unitValue" class="fa fa-arrow-up"></i></a> Valor Unit <a v-on:click="arrowClick"><i id="arrow-down-unitValue" class="fa fa-arrow-down"></i></a>
                </th>
                <th scope="col">
                    <a v-on:click="arrowClick"><i id="arrow-up-saleDate" class="fa fa-arrow-up"></i></a>  Data da Venda <a v-on:click="arrowClick"><i id="arrow-down-saleDate" class="fa fa-arrow-down"></i></a>
                </th>
            </tr>
        </thead>

        <tbody>
            <tr v-for="item, index in resultData" v-bind:key="index">
                <td class="col-sm-3">
                    {{ item.barCode }}
                </td>
                <td class="col-auto">
                    {{ item.name }}
                </td>
                <td class="col-sm-1">
                    {{ item.amount }}
                </td>
                <td class="col-sm-2">
                    R$ {{ item.unitValue }}
                </td>
                <td class="col-sm-2">
                    {{ item.saleDate}}
                </td>
            </tr>
        </tbody>
    </table>
</section>


<Footer></Footer>
</template>

<script>
import Header from '../../components/Header.vue'
import Footer from '../../components/Footer.vue'
import axios from 'axios'
import mixins from '../../mixins.js'
import $ from 'jquery'
export default {
  name: 'Home',
  components: {
    Header,
    Footer
  },
  mixins: [mixins],
  data() {
    return{
      defaultUrl: "/Cashier/Index",
    }
  },
  mounted() {
    this.Index()
  },
  methods: {
    //Obtem o id da seta clicada e adiciona o id na url para exibir a lista ordenada
    arrowClick: function(e) {
        let id = $(e.currentTarget).children()
        let value = id.attr("id")
        this.resultData = []
        this.pages = []
        this.defaultUrl += "/?arrowId=" + value
        this.Index()
        this.defaultUrl = "/Cashier/Index"
    },
    //Lista todas as vendas
    async Index() {
      axios
      .get(this.defaultUrl)
          .then((response) => {
              response.data.forEach(element => {
                  let obj = {
                      barCode: element.barCode,
                      name: element.name,
                      amount: element.amount,
                      unitValue: this.$options.filters.currencydecimal(element.unitValue, false),
                      saleDate: this.$options.filters.dateTimeFormat(element.saleDate)
                  }
                  this.resultData.push(obj)
              })
      })
      .catch(() => {
          this.errored = true
      })
      .finally(() => this.loading = false)
    },
  },
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .fa-arrow-up, .fa-arrow-down {
      color: #3d1e8f;
      cursor: pointer;
  }

  .fa-arrow-up {
      color: #5d9f3f;
  }
  .fa-arrow-down {
      color: #0d3c9e;
  }

  .fa-arrow-up:hover, .fa-arrow-down:hover {
      animation: StopMoveUpDown;
  }

  .fa-arrow-down, .fa-arrow-up {
      animation: MoveUpDown 1s linear infinite;
      position: relative;
      left: 0;
      bottom: 0;
  }

  .fa-edit, .fa-trash, .fa-list{
      font-size: 20px;
  }

  .fa-edit, .fa-trash:hover, .fa-list {
      cursor: pointer;
  }

  .fa-edit {
      color: #2680ed;
  }

  @keyframes MoveUpDown {
    0%, 100% {
        bottom: 0;
    }

    100% {
        bottom:3px;
    }
  }

  @keyframes StopMoveUpDown {
      0%, 100% {
          bottom: 0;
      }

      100% {
          bottom:0;
      }
  }
</style>