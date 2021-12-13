<template>
  <Header></Header>

<section class="container-fluid" style="margin-top: 100px;">
    <div v-if="loading">
        <div class="spinner-border text-primary float-lg-right mb-2" role="status">
            <span class="sr-only">Carregando...</span>
        </div>
    </div>

    <!-- Modal -->
        <div class="modal fade" ref="deleteItemModal" tabindex="-1" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title" id="modalLabel">Confirmar exclusão do item</h5>
            <button type="button" class="btn-close" @click="modal.hide()" aria-label="Close"></button>
        </div>
        <div class="modal-body">
            <h4 class="text-center">Deseja realmente excluir o item abaixo?</h4>
            <hr>
            <ul class="list-group list-group-flush" v-for="item, index in list" v-bind:key="index">
                <li class="list-group-item" >Código de Barras: {{ item.barCode }}</li>
                <li class="list-group-item" >Nome: {{ item.name }}</li>
            </ul>
        </div>
        <div v-if="success">
            <p class="alert alert-danger text-center m-3">{{ alertSuccess }}</p>
        </div>
        <div class="modal-footer">

            <button type="button" class="btn btn-secondary" @click="modal.hide()">Cancelar</button>
            <button type="button" class="btn btn-primary" v-on:click="deleteItem(productId)">Excluir</button>
        </div>
        
        </div>
    </div>
    </div>

    <table class="table table-bordered table-hover mt-5" id="tblListProducts">
        <thead>
            <tr class="bg-light">
                <th colspan="6" class="text-center"><h2 class="text-center">Lista de Produtos</h2></th>
                <th class="text-center">
                    <router-link class="font-weight-bold" to="/Estoque/Criar"><i class="fa fa-plus fa-2x p-1"></i></router-link>
                </th>
            </tr>
        </thead>
        <thead class="text-center">
            <tr>
                <th>
                    <a v-on:click="arrowClick"><i id="arrow-up-barCode" class="fa fa-arrow-up"></i></a> Código de Barras <a v-on:click="arrowClickProduto"><i id="arrow-down-barCode" class="fa fa-arrow-down"></i></a>
                </th>
                <th>
                    <a v-on:click="arrowClick"><i id="arrow-up-name" class="fa fa-arrow-up"></i></a> Nome <a v-on:click="arrowClickProduto"><i id="arrow-down-name" class="fa fa-arrow-down"></i></a>
                </th>
                <th>
                    <a v-on:click="arrowClick"><i id="arrow-up-amount" class="fa fa-arrow-up"></i></a> Quantidade <a v-on:click="arrowClickProduto"><i id="arrow-down-amount" class="fa fa-arrow-down"></i></a>
                </th>
                <th>
                    <a v-on:click="arrowClick"><i id="arrow-up-unitValue" class="fa fa-arrow-up"></i></a> Valor Unitário <a v-on:click="arrowClickProduto"><i id="arrow-down-unitValue" class="fa fa-arrow-down"></i></a>
                </th>
                <th>
                    <a v-on:click="arrowClick"><i id="arrow-up-expirationDate" class="fa fa-arrow-up"></i></a> Data de Validade <a v-on:click="arrowClickProduto"><i id="arrow-down-expirationDate" class="fa fa-arrow-down"></i></a>
                </th>
                <th>
                    <a v-on:click="arrowClick"><i id="arrow-up-registerDate" class="fa fa-arrow-up"></i></a> DataRegistro <a v-on:click="arrowClickProduto"><i id="arrow-down-registerDate" class="fa fa-arrow-down"></i></a>
                </th>
                <th colspan="2">
                    Opções / Ações
                </th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="item, index in resultData" v-bind:key="index">
                <td>
                    {{ item.barCode }}
                </td>
                <td>
                    {{ item.name }}
                </td>
                <td>
                    {{ item.amount }}
                </td>
                <td>
                    {{ item.unitValue }}
                </td>
                <td>
                    {{ item.expirationDate }}
                </td>
                <td>
                    {{ item.registerDate }}
                </td>
                <td class="text-center" colspan="67">
                    <router-link :to="'Editar/?id=' + item.estoqueId"  class="p-2"><span class="fa fa-edit"></span></router-link>
                    <router-link to="/" class="p-2"><i class="fa fa-list"></i></router-link>
                    <a href="#" v-on:click="getDeleteItems(index)"><i class="fa fa-trash"></i></a>
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
import { Modal } from 'bootstrap'
import axios from 'axios'
import mixins from '../../mixins.js'

export default {
  name: 'Home',
  components: {
    Header,
    Footer
  },   
  mixins: [mixins], 
  mounted: function () {
      this.Index()
      this.modal = new Modal(this.$refs.deleteItemModal)
  },
  data() {
      return{
        defaultUrl: "/Inventory/Index",
        productId: null,
        modal: null,
        listIndex: null
      }
  },    
  methods: {
    //Lista todos os produtos  
    async Index() {
    await axios
    .get(this.defaultUrl)
        .then((response) => {
            response.data.forEach(element => {
                let obj = {
                    estoqueId: element.inventoryId,
                    barCode: element.barCode,
                    name: element.name,
                    amount: element.amount,
                    unitValue: this.$options.filters.currencydecimal(element.unitValue, false),
                    expirationDate: this.$options.filters.dateFormat(element.expirationDate),
                    registerDate: this.$options.filters.dateTimeFormat(element.registerDate),
                }
                this.resultData.push(obj)
            })
        })
        .catch(() => {
            this.errored = true
        })
        .finally(() => this.loading = false)
    },
    //Obtem o item da tabela a ser deletado e o exibi na lista modal
    getDeleteItems: function (index) {
        this.modal.show()
        this.list.splice(0, this.list.length)
        this.listIndex = index
        let items = {
            barCode: this.resultData[index].barCode,
            name: this.resultData[index].name
        }
        this.productId = this.resultData[index].estoqueId
        this.list.push(items)
    },
    //Esconde o modal
    hideModal: function(){
        setTimeout(() => this.modal.hide(), 3000)
    },
    //Deleta o item exibido na lista modal pelo inventoryId
    async deleteItem(id){
        await axios
        .delete("/Inventory/Excluir/?id=" + id)
            .then((response) => {
                this.success = true
                this. alertSuccess = response.data
                setTimeout(() => this.success = false, 3000)
                this.hideModal()
                setTimeout(() => this.resultData.splice(this.listIndex, 1), 3000)  
            })
            .catch(() => {
                this.errored = true
            })
            .finally(() => this.loading = false)
    }, 
  },
}
</script>