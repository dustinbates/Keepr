<template>
  <div class="container-fluid">
    <div class="row p-md-5 ms-md-3">
      <h1>Vaults</h1>
      <div v-for="v in vaults" class="col-6 col-md-3 mb-4">
        <VaultCard :vault="v" />
      </div>
    </div>
    <div class="row px-5 ms-3">
      <h1>Keeps</h1>
    </div>
    <div class="bricks">
      <div v-for="k in keeps">
        <KeepCard :keep="k" />
      </div>
    </div>
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
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { profilesService } from '../services/ProfilesService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { useRoute, useRouter } from 'vue-router';
import { vaultsService } from '../services/VaultsService'

export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    onMounted(() => {
      getProfileById();
    })
    watchEffect(() => {
      getKeeps();
      getVaults();
    })
    async function getProfileById() {
      try {
        const profileId = route.params.profileId
        await profilesService.getProfileById(profileId);
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }
    async function getKeeps() {
      try {
        const id = AppState.profile.id
        await keepsService.getProfileKeeps(id)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }
    async function getVaults() {
      try {
        const id = AppState.profile.id
        await vaultsService.getProfileVaults(id)
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }
    return {
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.profileVaults),
    }
  }
}
</script>

<style lang="scss" scoped>
img {
  max-width: 100px;
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
