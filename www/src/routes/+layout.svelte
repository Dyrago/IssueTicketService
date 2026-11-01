<script lang="ts">
	import favicon from '$lib/assets/favicon.svg';
	import '../lib/styles/styles.css'
	import type { LayoutProps } from './$types';
	import { logout } from '$lib/api/logout'

	let { data, children } : LayoutProps = $props();


	const openSidebar = () =>
	{
		let sidebar = document.getElementById("sidebar");

		if(sidebar)
		{
			sidebar.style.width = "40vw";
		}	
	}

	const closeSidebar = () =>
	{
		let sidebar = document.getElementById("sidebar");

		if(sidebar)
			sidebar.style.width = "0px";
	}
</script>

<svelte:head>
	<link rel="icon" href={favicon} />
</svelte:head>

<div>
	<header>
		<a href="/Login"><img id="logo" src="../src/lib/assets/favicon.svg" alt="Icon"></a>
		{#if data.user}
		<button id="uprof" onclick={openSidebar}>
			<h4 id="uname">{data.user.email}</h4>
		</button>
		{/if}
	</header>

	{@render children()}

	<footer id="footer">
		Created by ®Dyrago
	</footer>
</div>

{#if data.user}
<div id="sidebar">
	<div id="sidebar-opts">
		<span class="closebtn" onclick= {closeSidebar}>&times;</span>
		<a href="Profile" class="opt-item">Profile</a>
		<a href="Settings" class="opt-item">Settings</a>
		<button class="opt-item" onclick={logout}>Logout</button>
	</div>
</div>
{/if}

<style>

	#logo{
		height: 75px;
		width: 55px;
	}

	#uprof{
		background-color: #333;
		border-radius: 20px;
		border: 0.5px solid #000;
		cursor: pointer;
		padding: 0.5rem;
		grid-column-start: 11;
	}

	#uname{
		color: #fff;
	}
	
	header{
		border: 1px solid black;
		padding: 0.45rem;
		color: #fff;
		background-color: rgb(28, 28, 221);
		display: grid;
		grid-template-columns: repeat(12, 1fr);
		align-items: center;
	}

	footer{
		padding: 1rem;
		background-color: #333;
		color: #fff;
		font-size: medium;
		font-weight: 500;
		text-align: center;
	}
	
	#sidebar{
		top: 0;
		right: 0;
		position: fixed;
		background-color: #333;
		color: #fff;
		height: 100%;
		z-index: 1;
		width: 0;
		overflow-x: hidden;
		overflow-y: scroll;
		transition: 0.5s;
		padding-top: 1rem;
	}

	.closebtn{
		font-size: 3.5rem;
		cursor: pointer;
		grid-column: span 4;
		text-align: center;
	}

	.closebtn:hover{
		color: #9c9c9c;
	}

	#sidebar-opts{
		display: grid;
		grid-template-columns: repeat(12, 1fr);
	}

	#sidebar-opts > button{
		cursor: pointer;
		font-size: large;
	}

	#sidebar-opts > a{
		cursor: pointer;
		direction: none;
		color: white;
		text-decoration: none;
		font-size: large;
	}

	#sidebar-opts > a:hover{
		background-color: #4e4e4e;
	}

	.opt-item
	{
		text-align: center;
		padding: 1rem;
		cursor: pointer;
		margin-bottom: 0.5rem;
		grid-column: span 12;
	}
	

</style>