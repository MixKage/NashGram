<template>
  <header class="header">
    <v-toolbar>
      <H1>NashGram</H1>
      <v-spacer></v-spacer>
      <v-toolbar-title v-if="this.$store.getters.GET_AUTH">
        <v-btn @click="dialog" text>{{ this.name }}</v-btn></v-toolbar-title
      >
      <v-btn @click="dialog" v-if="this.$store.getters.GET_AUTH" icon>
        <v-icon>mdi-account-circle</v-icon>
      </v-btn>
      <router-link v-else class="link header__link" :to="{ path: '/login' }"
        ><v-btn text class="text-h6">Войти</v-btn></router-link
      >
    </v-toolbar>
    <DialogPerData v-bind:getName="getName" />
  </header>
</template>

<script>
import DialogPerData from "./DialogPerData";
import { HTTP } from "../api/API";
export default {
  components: { DialogPerData },
  data: () => ({
    name: "",
  }),
  methods: {
    dialog() {
      this.$store.dispatch("SET_PERDIALOG", !this.$store.getters.GET_PERDIALOG);
    },
    getName() {
      const token = localStorage.getItem("token");
      HTTP.get("GetName", {
        headers: {
          Authorization: `Basic ${token}`,
        },
      })
        .then((res) => {
          this.name = res.data;
          console.log(this.name);
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
  mounted() {
    this.getName();
  },
};
</script>

<style scoped>
.header__link {
  text-decoration: none;
}
</style>
