/*Mixing com todas as variaveis, metodos e filtros que são utilizados em varias partes
do sistema*/

const mixins = {
    data() {
        return{
          Id: 0,
          barCode: "",
          name: "",
          amount: "",
          unitValue: "",
          list: [],
          listItems: {},
          alertSuccess: "",
          success: false,
          alertDanger: "",
          error: false,
          formFieldError: false,
          loading: true,
          errorBarCode: "",
          errorName: "",
          errorAmount: "",
          errorUnitValue: "",
          listErrorMessage: "",
          listEmpty: false,
          resultData: [],
          pages: [],
        }
      }, 
      filters: {
        //Substitu a virgula por ponto ou ponto por virgula          
        currencydecimal(value, reverse) {
            let currency = ""

            if (reverse == true) {
                currency = parseFloat(value.replace(",", "."))
            }
            else {
                currency = parseFloat(value).toFixed(2).replace(".", ",")
            }
            return currency
          },
        //Formata a data para data local          
        dateFormat(value) {
            let dateString = new Date(value).toLocaleDateString()
            return dateString.toString()
          },
        //Formata a data para data local com tempo        
        dateTimeFormat(value) {
            let dateTimeString = new Date(value).toLocaleDateString() + " " + new Date(value).toLocaleTimeString()
            return dateTimeString.toString()
        },
        //Formata o campo com type date para ser exibido corretamente no input        
        fieldTypeDateFormat(value) {
            let date = new Date(value)
            let dateTimeString = date.getFullYear() + "-" + ("0" + date.getMonth() + 1).slice(-2) + "-" + ("0" + date.getDate()).slice(-2)
            return dateTimeString.toString()
        },
    },
    methods: {
        //Valida todos os campos do formulário
        validateForm() {
            let msg = "Preencha este campo!"
            if(this.barCode == ""){
                this.formFieldError = true
                this.errorbarCode = msg
                return false
            }
            else{
                this.errorbarCode = ""
                this.formFieldError = false
            }
            if(this.name == ""){
                this.formFieldError = true
                this.errorName = msg
                return false
            }
            else{
                this.errorName = ""
                this.formFieldError = false
            }
            if(this.amount == ""){
                this.formFieldError = true
                this.errorAmount = msg
                return false
            }
            else{
                this.errorAmount = ""
                this.formFieldError = false
            }
            if(this.unitValue == ""){
                this.formFieldError = true
                this.errorUnitValue = msg
                return false
            }
            else{
                this.errorUnitValue = ""
                this.formFieldError = false
            }
           
            return true
        }
    },
}

export default mixins