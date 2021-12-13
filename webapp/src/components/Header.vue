<template>
    <nav class="navbar navbar-expand-lg navbar-light bg-white fixed-top border-bottom shadow">
    <div class="container-fluid">
        <router-link class="navbar-brand font-weight-bold mx-5" to="/Caixa/Criar">CAIXAFACIL</router-link>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse mx-5" id="navbarNavDropdown">
        <ul class="navbar-nav">
            <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle" href="/Caixa/Criar" id="navbarDropdownMenuLink" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                    Frente de Caixa
                </a>
                <ul class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                    <li>
                        <router-link class="dropdown-item" to="/Caixa/Criar">Abrir Caixa</router-link></li>
                    <li>
                        <router-link class="dropdown-item" to="/Caixa/Listar">Produtos Vendidos</router-link>
                    </li>
                </ul>
            </li>

            <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                    Estoque
                </a>
                <ul class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                    <li>
                        <router-link class="dropdown-item" to="/Estoque/Criar">Cadastrar</router-link></li>
                    <li>
                        <router-link class="dropdown-item" to="/Estoque/Listar">Listar</router-link>
                    </li>
                </ul>
            </li>
        </ul>
        </div>
    </div>
    </nav>
</template>

<script>

export default {
  name: 'Header',
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
