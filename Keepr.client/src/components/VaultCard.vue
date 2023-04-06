<template>
  <div class="vaultCard rounded">
    <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
      <img class="vaultImg img-fluid rounded" title="View Vault" :src="vault.img" :alt="vault.name">
    </router-link>
    <p class="overlay">{{ vault.name }}</p>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed } from 'vue';

export default {
  props: { vault: { type: Object, required: true } },
  setup() {
    return {
      vaults: computed(() => AppState.profileVaults),
    }
  }
}
</script>


<style lang="scss" scoped>
.vaultCard {
  position: relative;
}

.vaultImg {
  height: 20vh;
  width: 100%;
  object-fit: cover;
  box-shadow: 0px 6px 5px rgba(0, 0, 0, 0.478);
}

.overlay {
  position: absolute;
  text-align: start;
  font-family: fantasy;
  font-size: x-large;
  margin: .5em;
  bottom: 0;
  color: rgb(240, 234, 244);
  text-shadow: 1px 1px 2px rgb(0, 0, 0);
  width: 50%;
  overflow: hidden;
  text-overflow: ellipsis;
}
</style>