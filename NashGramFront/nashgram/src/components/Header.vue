<template>
  <header class="header">
    <v-toolbar>
      <H1>NashGram</H1>
      <v-spacer></v-spacer>
      <v-toolbar-title v-if="this.$store.getters.GET_AUTH"
        >UserName</v-toolbar-title
      >
      <v-btn v-if="this.$store.getters.GET_AUTH" icon>
        <v-icon>mdi-account-circle</v-icon>
      </v-btn>
      <router-link v-else class="link header__link" :to="{ path: '/login' }"
        ><v-btn text class="text-h6">Войти</v-btn></router-link
      >
    </v-toolbar>
  </header>

</template>

<script></script>

<style scoped>
.header__link {
  text-decoration: none;
}
</style>
