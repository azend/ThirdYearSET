
print 'What is X? '
x = gets.chomp!
x = x.to_f

unless x > 0 
	abort('ERROR: Did not enter a value greater than 0')
end

print 'What is K? '
k = gets.chomp!
k = k.to_i

unless k > 0 
	abort('ERROR: Did not enter a value greater than 0')
end

i = 1
sum = 0

while i <= k do
	sum += ((x * 0.5) ** (i - 1))

	puts "x = #{ x }, k = #{ k }, i = #{ i }, sum = #{ sum }"

	i += 1
end