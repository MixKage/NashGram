<template>
  <v-dialog v-model="errDialog" width="20%">
    <v-card class="error">
      <v-card-title class="text-h5 lighten-2" @click="close">
        Ошибка!
      </v-card-title>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  data() {
    return {
      errDialog: false,
    };
  },
  methods: {
    close() {
      this.$store.dispatch("SET_ERRDIALOG", false);
    },
  },
  watch: {
    "$store.state.errDialog": {
      handler: function () {
        this.errDialog = this.$store.getters.GET_ERRDIALOG;
      },
      immediate: true, // provides initial (not changed yet) state
    },
  },
};
</script>
<style scoped>
.error {
  display: flex;
  justify-content: center;
  align-items: center;
  text-align: center;
}
</style>
