<template>
  <header class="header">
    <v-toolbar>
      <H1>NashGram</H1>
      <v-spacer></v-spacer>
      <v-toolbar-title v-if="this.$store.getters.GET_AUTH">
        <v-btn @click="dialog" text>{{ this.name }}</v-btn></v-toolbar-title
      >
      <v-btn @click="dialog" v-if="this.$store.getters.GET_AUTH" icon>
        <v-img

          height="50px"
          :aspect-ratio="16 / 9"
          class="card__image"
          v-bind:src="`${this.avatar}`"
        ></v-img>
      </v-btn>
      <router-link v-else class="link header__link" :to="{ path: '/login' }"
        ><v-btn text class="text-h6">Войти</v-btn></router-link
      >
    </v-toolbar>
    <DialogPerData v-bind:getName="setData" />
  </header>
</template>

<script>
import DialogPerData from "./DialogPerData";
import { HTTP } from "../api/API";
export default {
  components: { DialogPerData },
  data: () => ({
    name: "",
    avatar: "",
    currUser: null,
  }),
  methods: {
    dialog() {
      this.$store.dispatch("SET_PERDIALOG", !this.$store.getters.GET_PERDIALOG);
    },
    async setData() {
      const token = localStorage.getItem("token");
      console.log(token);
      HTTP.get("GetPerson", {
        headers: {
          Authorization: `Basic ${token}`,
        },
      })
        .then((res) => {
          this.name = res.data.name;
          this.avatar = res.data.avatar;
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
  mounted() {
    this.setData();
  },
  watch: {
    "$store.state.curruser": {
      handler: function () {
        this.currUser = this.$store.getters.GET_CURRUSER;
        console.log(this.currUser.id);
        this.setData();
      },
      immediate: true, // provides initial (not changed yet) state
    },
  },
};
</script>

<style scoped>
.header__link {
  text-decoration: none;
}
.card__image{
  border-radius: 500px;
}
</style>
