
# PROG3160 - Numerical Methods 3000
# Assignment #1
# Verdi R-D

# Get the x value from the user
print 'What is X? '
x = gets.chomp!
x = x.to_f

unless x > 0 
	abort('ERROR: Did not enter a value greater than 0')
end

# Get the k value from the user
print 'What is K? '
k = gets.chomp!
k = k.to_i

unless k > 0 
	abort('ERROR: Did not enter a value greater than 0')
end

i = 1
sum = 0

# Calculate the fraction
fraction = 2.0 / ( 2.0 - x )

puts '' 
puts "Fraction = #{ sprintf '%.6f', fraction }"
puts ''
puts 'Approximations:'

# Calculate our approximations
while i <= k do
	sum += ((x * 0.5) ** (i - 1))
	absolute_error = ( fraction - sum ).abs
	relative_error = ( absolute_error / fraction ) * 100

	puts "x = #{ x }, k = #{ k }, i = #{ i }, sum = #{ sprintf "%.6f", sum } error = #{ sprintf "%.2e", relative_error }"

	i += 1
end
