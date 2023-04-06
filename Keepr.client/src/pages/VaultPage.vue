<template>
  <div v-if="vault.id" class="container-fluid p-3">
    <div class="header d-flex justify-content-center">
      <div v-if="account.id == vault.creator.id" type="button" class="overlayTopRight">
        <i class="fs-1 mdi mdi-delete-outline" title="Delete Vault" @click="deleteVault(vault.id)"></i>
      </div>
      <!-- <div class="dropdown-menu text-center fs-1">
        <div class="d-flex justify-content-evenly">
          <i class="mdi mdi-delete-outline fs-1 text-danger" title="delete vault" @click="deleteVault(vault.id)"></i>
        </div>
      </div> -->
      <img class="vaultImg" :src="vault.img" :alt="vault.name">
      <p class="overlay mb-0 text-center">{{ vault.name }} <br> by {{ vault.creator.name }}</p>
    </div>
    <section class="d-flex justify-content-center">
      <p class="fs-1 m-0 mb-5 p-0">{{ vaultKeeps.length }} Keeps</p>
    </section>
    <section class="bricks">
      <div v-for="vk in vaultKeeps" class="delete fs-3">
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
import { computed, watchEffect, onMounted } from 'vue';
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
    onMounted(() => {
      getVaultKeeps();
    })
    watchEffect(() => {
      if (route.params.vaultId) {
        getVaultById();
        getVaultKeeps();
      }
    })
    return {
      route,
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault),
      vaultKeeps: computed(() => AppState.vaultKeeps),

      async deleteVault(vaultId) {
        try {
          if (await Pop.confirm('Delete this Vault?', "Are you sure you want to delete this Vault? You won't be able to revert this.", 'Delete', 'question')) {
            await vaultsService.deleteVault(vaultId)
            router.push({ name: 'Account' })
          }
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
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
  width: 75%;
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
  width: 50%;
  overflow: hidden;
  text-overflow: ellipsis;
}

.overlayTopRight {
  position: absolute;
  top: 0;
  right: 0;
  text-align: center;
  color: white;
  text-shadow: 1px 1px 2px black;
  width: 35%;
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