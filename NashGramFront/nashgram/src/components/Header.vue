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
          height="30px"
          :aspect-ratio="16 / 9"
          class="card__image"
          v-bind:src="`${this.avatar}`"
        ></v-img>
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
    avatar: "",
    currUser: null,
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
        })
        .catch((err) => {
          console.log(err);
        });
    },
    getAvatar() {
      const token = localStorage.getItem("token");
      HTTP.get("GetAvatarFromId", {
        params: { id: this.currUser.id },
        headers: {
          Authorization: `Basic ${token}`,
        },
      })
        .then((res) => {
          this.avatar = res.data;
        })
        .catch((err) => {
          console.log(err.status);
        });
    },
  },
  mounted() {
    console.log(this.$store.getters.GET_CURRUSER.id);
    this.getName();
    this.getAvatar();
  },
  watch: {
    "$store.state.curruser": {
      handler: function () {
        this.currUser = this.$store.getters.GET_CURRUSER;
        console.log(this.currUser);
        this.getAvatar();
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
</style>
