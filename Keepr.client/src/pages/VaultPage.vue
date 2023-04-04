<template>
  <div class="container-fluid p-3">
    <section class="header d-flex justify-content-center">
      <img class="vaultImg" :src="vault.img" :alt="vault.name">
      <p class="overlay text-center">{{ vault.name }} <br> by {{ vault.creator.name }}</p>
    </section>
    <section class="d-flex justify-content-center">
      <p class="fs-1 m-0  mb-5 p-0">{{ vaultKeeps.length }} Keeps</p>
    </section>
    <section class="bricks">
      <div v-for="vk in vaultKeeps">
        <VaultKeepCard :vaultKeep="vk" />
      </div>
    </section>
    <Modal id="activeKeep">
      <ActiveKeep />
    </Modal>
    <Modal id="createKeep">
      <CreateKeepForm />
    </Modal>
    <Modal id="createVault">
      <CreateVaultForm />
    </Modal>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, watchEffect } from 'vue';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
import { useRoute, useRouter } from 'vue-router';

export default {
  setup() {
    const route = useRoute();
    const router = useRouter();

    async function getVaultById() {
      try {
        const vaultId = route.params.vaultId;
        await vaultsService.getVaultById(vaultId)
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
        router.push({ name: 'Home' })
      }
    }

    async function getVaultKeeps() {
      try {
        const vaultId = route.params.vaultId
        await vaultsService.getVaultKeeps(vaultId)
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }

    watchEffect(() => {
      if (route.params.vaultId) {
        getVaultById();
        getVaultKeeps();
      }
    })
    return {
      route,
      vault: computed(() => AppState.activeVault),
      vaultKeeps: computed(() => AppState.vaultKeeps)
    }
  }
}
</script>


<style lang="scss" scoped>
.header {
  position: relative;
}

.vaultImg {
  height: 40vh;
  width: 50%;
  object-fit: cover;
  border-radius: 30px;
}

.overlay {
  position: absolute;
  bottom: 0;
  color: whitesmoke;
  font-size: 50px;
  font-family: 'Times New Roman', Times, serif;
  text-shadow: 2px 2px 4px black;
}

.bricks {
  columns: 4 200px;
  column-gap: 1.5rem;
  margin-right: 5rem;
  margin-left: 5rem;

  @media screen and (max-width: 456px) {
    columns: 2;
    margin-right: .25rem;
    margin-left: .25rem;
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