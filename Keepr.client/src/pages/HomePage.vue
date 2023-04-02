<template>
  <div class="container-fluid p-4">
    <section class="bricks">
      <div v-for="k in keeps">
        <KeepCard :keep="k" />
      </div>
    </section>
  </div>
</template>

<script>
import { keepsService } from '../services/KeepsService.js';
import { onMounted, computed } from 'vue';
import { AppState } from '../AppState';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';


export default {
  setup() {
    onMounted(() => {
      getKeeps()
    })
    async function getKeeps() {
      try {
        await keepsService.getKeeps()
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }
    return {
      keeps: computed(() => AppState.keeps),
    }
  }
}
</script>

<style scoped lang="scss">
.bricks {
  columns: 4 200px;
  column-gap: 1.5rem;

  @media screen and (max-width: 456px) {
    columns: 2
  }

  &>div {
    width: 150px;
    color: white;
    margin: 0 1.5rem 1.5rem 0;
    display: inline-block;
    width: 100%;
  }

}
</style>
