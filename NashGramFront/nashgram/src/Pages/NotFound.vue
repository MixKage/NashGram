<template>
  <div id="notfound" class="notfound">
  <h1 class="notfound__title">Не найдено!</h1>
    <v-btn
        text
        elevation="2"
        large
        x-large
        @click="$router.go(-1)"
    >Вернуться к цивилизации...
    </v-btn>
  </div>
</template>
<script>
export default {
  components: {},
};
</script>

<style scoped>
.notfound {
  display: flex;
  width: 100%;
  height: 100%;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}

.notfound__title {
  margin: 50px auto;
  font-size: 100px;
  color: black;
}
</style>
