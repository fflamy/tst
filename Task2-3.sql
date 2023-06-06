SELECT c.clientname,count(cc)
	FROM public.clients c
	join public.clientcontacts cc
		on c.id = cc.clientid
	GROUP by c.clientname;

SELECT c.clientname,count(cc)
	FROM public.clients c
	join public.clientcontacts cc
		on c.id = cc.clientid
	GROUP by c.clientname
	Having count(cc)>2;
	
SELECT d.Id,Min(d.dt) as ST, Max(d.dt) as ED
from dates d
GROUP by d.Id;

