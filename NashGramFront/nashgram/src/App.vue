<template>
  <div id="app">
    <v-app>
      <router-view></router-view>
    </v-app>
  </div>
</template>

<script>
import { HTTP } from "./api/API";

export default {
  name: "App",

  components: {},

  data: () => ({

  }),
  created() {
    const token = localStorage.getItem("token");
    console.log(token);
    HTTP.get("GetPerson", {
      headers: {
        Authorization: `Basic ${token}`,
      },
    })
      .then((res) => {
        this.$store.dispatch("SET_CURRUSER", res.data);
        this.$store.dispatch("SET_AUTH", true);
        localStorage.setItem("token", token);
        console.log(this.$store.getters.GET_CURRUSER);
      })
      .catch((err) => {
        this.$store.dispatch("SET_AUTH", false);
        localStorage.setItem("token", "");
        console.log(err);
        this.err = true;
      });
  },
};
</script>
