<script setup>
import { useRouter } from 'vue-router'
import useAuth from '../auth';
import { ref } from 'vue';

/**
 * Represents the user's login model
 * @type {LoginRequest}
 */
const model = {
    email: "",
    password: ""
}

const router = useRouter()
const status = ref("")
const isLoggedIn = ref(false)
const modalRef = ref("dialog3")

const validateInputs = () => {

}

const formSubmit = async () => {
    validateInputs()
    try {
        const { login } = useAuth()
        console.log(login)
        await login(model)

        status.value = "Logged in successfully 😎. Redirecting ..."
        isLoggedIn.value = true
        modalRef.value.showModal()

        await new Promise(resolve => setTimeout(resolve, 1500))
        router.push({ name: 'userhome' })
    } catch (error) {
        console.error(error)
        status.value = "Couldn't Log in 😞"
        modalRef.value.showModal()
    }
}

</script>

<template>
    <div class="hero min-h-screen"
    style="background-image: url(https://images.unsplash.com/photo-1633265486064-086b219458ec?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D);">
        <div class="hero-overlay bg-opacity-60"></div>
        <div class="hero-content text-center">
            <div class="card shrink-0 w-full max-w-sm shadow-2xl bg-base-100">
                <div class="max-w-md card-body">
                    <h1 class="mb-5 text-5xl font-bold">Login</h1>
                    <!-- <p class="mb-5"></p> -->
                    <form ref="formRef"
                    @submit.prevent="formSubmit()">

                        <div class="form-control">
                            <label class="label">
                                <span class="label-text">Email</span>
                            </label>
                            <input v-model="model.email"
                            type="email" 
                            placeholder="email" 
                            class="input input-bordered" 
                            required />
                        </div>

                        <!-- FIXME(Validation; password must contain special character& a number, ) -->
                        <div class="form-control">
                            <label class="label">
                                <span class="label-text">Password</span>
                            </label>
                            <input v-model="model.password"
                            type="password"
                            placeholder="password" 
                            class="input input-bordered" 
                            required />
                        </div>
                        
                        <div class="form-control mt-6">

                            <!-- FIXME(:disabled="isFormInvalid") -->
                            <button class="btn btn-primary"
                            type="submit">Login
                            </button>

                            <dialog ref="modalRef" class="modal modal-bottom sm:modal-middle">
                                
                                <div class="modal-box relative h-40">
                                    <span class="absolute w-50 top-1/2 left-1/2 translate-y-[-50%] translate-x-[-50%] 
                                    loading loading-infinity loading-lg" 
                                    :class="isLoading?'':'hidden' "></span>
                                    
                                    <p :class="isLoading?'hidden':'' ">{{ status }}</p>

                                    <div class="modal-action" :class="isLoggedIn? 'hidden': '' ">
                                        <form method="dialog">
                                            <button class="btn btn-primary">Retry</button>
                                        </form>
                                    </div>
                                </div>
                            </dialog>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped></style>